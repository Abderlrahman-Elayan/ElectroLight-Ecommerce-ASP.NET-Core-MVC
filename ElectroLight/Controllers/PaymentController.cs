using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Domain.Entities;
using ElectroLight.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ElectroLight.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _uow;
        private readonly UserManager<ApplicationUser> _userManager;

        public PaymentController(
            IConfiguration configuration,
            IUnitOfWork uow,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _uow = uow;
            _userManager = userManager;
        }

        // =====================================================
        // STEP 1: CREATE PAYPAL ORDER
        // =====================================================
        public async Task<IActionResult> Pay(int orderId)
        {
            var user = await _userManager.GetUserAsync(User);

            var order = await _uow.Orders.GetAsync(
                o => o.Id == orderId && o.UserId == user.Id
            );

            if (order == null)
                return NotFound();

            var client = new HttpClient();

            var clientId = _configuration["PayPal:ClientId"];
            var secret = _configuration["PayPal:Secret"];
            var baseUrl = _configuration["PayPal:Url"];
            var baseAppUrl = $"{Request.Scheme}://{Request.Host}";

            // =========================
            // 1. GET ACCESS TOKEN
            // =========================
            var auth = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{clientId}:{secret}")
            );

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            var tokenResponse = await client.PostAsync(
                $"{baseUrl}/v1/oauth2/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" }
                })
            );

            var tokenJson = await tokenResponse.Content.ReadAsStringAsync();

            var accessToken = JsonDocument.Parse(tokenJson)
                .RootElement
                .GetProperty("access_token")
                .GetString();

            // =========================
            // 2. CREATE PAYPAL ORDER
            // =========================
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var requestBody = new
            {
                intent = "CAPTURE",
                purchase_units = new[]
                {
                    new
                    {
                        amount = new
                        {
                            currency_code = "USD",
                            value = order.TotalPrice.ToString("0.00")
                        }
                    }
                },
                application_context = new
                {
                    return_url = $"{baseAppUrl}/Payment/Success?orderId={order.Id}",
                    cancel_url = $"{baseAppUrl}/Payment/Cancel?orderId={order.Id}"
                }
            };

            var response = await client.PostAsync(
                $"{baseUrl}/v2/checkout/orders",
                new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                )
            );

            var json = await response.Content.ReadAsStringAsync();

            var root = JsonDocument.Parse(json).RootElement;

            var paypalOrderId = root.GetProperty("id").GetString();

            var approvalUrl = root
                .GetProperty("links")
                .EnumerateArray()
                .First(x => x.GetProperty("rel").GetString() == "approve")
                .GetProperty("href")
                .GetString();

            order.PayPalOrderId = paypalOrderId;
            _uow.Orders.Update(order);
            await _uow.SaveChangesAsync();

            return Redirect(approvalUrl);
        }

        // =====================================================
        // STEP 2: SUCCESS (CAPTURE PAYMENT)
        // =====================================================
        public async Task<IActionResult> Success(int orderId)
        {
            var user = await _userManager.GetUserAsync(User);

            var order = await _uow.Orders.GetAsync(
                o => o.Id == orderId && o.UserId == user.Id
            );

            if (order == null)
                return NotFound();

            var client = new HttpClient();

            var clientId = _configuration["PayPal:ClientId"];
            var secret = _configuration["PayPal:Secret"];
            var baseUrl = _configuration["PayPal:Url"];

            // =========================
            // 1. GET TOKEN AGAIN
            // =========================
            var auth = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{clientId}:{secret}")
            );

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic", auth);

            var tokenResponse = await client.PostAsync(
                $"{baseUrl}/v1/oauth2/token",
                new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "grant_type", "client_credentials" }
                })
            );

            var tokenJson = await tokenResponse.Content.ReadAsStringAsync();

            var accessToken = JsonDocument.Parse(tokenJson)
                .RootElement
                .GetProperty("access_token")
                .GetString();

            // =========================
            // 2. CAPTURE PAYMENT (FIXED)
            // =========================
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);

            var captureResponse = await client.PostAsync(
                $"{baseUrl}/v2/checkout/orders/{order.PayPalOrderId}/capture",
                new StringContent("{}", Encoding.UTF8, "application/json") // 🔥 FIX
            );

            var captureJson = await captureResponse.Content.ReadAsStringAsync();

            var captureRoot = JsonDocument.Parse(captureJson).RootElement;

            var status = captureRoot.GetProperty("status").GetString();

            if (status != "COMPLETED")
            {
                order.PaymentStatus = PaymentStatus.Failed;
                _uow.Orders.Update(order);
                await _uow.SaveChangesAsync();

                return RedirectToAction("Index", "Checkout");
            }

            var transactionId = captureRoot
                .GetProperty("purchase_units")[0]
                .GetProperty("payments")
                .GetProperty("captures")[0]
                .GetProperty("id")
                .GetString();

            // =========================
            // 3. SAVE PAYMENT
            // =========================
            var payment = new Payment
            {
                OrderId = order.Id,
                TransactionId = transactionId ?? "",
                Amount = order.TotalPrice,
                Status = PaymentStatus.Paid,
                CreatedAt = DateTime.UtcNow
            };

            await _uow.Payments.AddAsync(payment);

            // =========================
            // 4. UPDATE ORDER
            // =========================
            order.Status = OrderStatus.Processing;
            order.PaymentStatus = PaymentStatus.Paid;
            order.PaymentDate = DateTime.UtcNow;

            // =========================
            // 5. CLEAR CART
            // =========================
            var cart = await _uow.ShoppingCarts.GetBetterVersionAsync(
                c => c.UserId == user.Id,
                include: q => q.Include(c => c.cartItems)
            );

            _uow.CartItems.RemoveRange(cart.cartItems);

            _uow.Orders.Update(order);
            await _uow.SaveChangesAsync();

            return RedirectToAction("Confirmation", "Checkout", new { id = orderId });
        }

        // =====================================================
        // STEP 3: CANCEL
        // =====================================================
        public async Task<IActionResult> Cancel(int orderId)
        {
            var user = await _userManager.GetUserAsync(User);

            var order = await _uow.Orders.GetAsync(
                o => o.Id == orderId && o.UserId == user.Id
            );

            order.Status = OrderStatus.Cancelled;
            order.PaymentStatus = PaymentStatus.Failed;

            _uow.Orders.Update(order);
            await _uow.SaveChangesAsync();

            return RedirectToAction("Index", "Checkout");
        }
    }
}