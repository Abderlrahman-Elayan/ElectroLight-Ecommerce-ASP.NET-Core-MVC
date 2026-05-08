using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Domain.Entities;
using ElectroLight.Domain.Enums;
using ElectroLight.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroLight.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(
            IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            var cart = await _unitOfWork.ShoppingCarts.GetBetterVersionAsync(
                c => c.UserId == user.Id,
                include: q => q
                    .Include(c => c.cartItems)
                    .ThenInclude(ci => ci.Product)
            );

            if (cart == null || !cart.cartItems.Any())
                return RedirectToAction("Index", "ShoppingCart");

            var vm = new CheckoutVM
            {
                Cart = cart,
                Address = "",
                PhoneNumber = ""
            };

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(CheckoutVM model)
        {
            var user = await _userManager.GetUserAsync(User);

            var cart = await _unitOfWork.ShoppingCarts.GetBetterVersionAsync(
                c => c.UserId == user.Id,
                AsTracking: true,
                include: q => q
                    .Include(c => c.cartItems)
                    .ThenInclude(ci => ci.Product)
            );

            if (cart == null || !cart.cartItems.Any())
                return BadRequest("Cart is empty");

            if (!ModelState.IsValid)
            {
                model.Cart = cart;
                return View("Index", model);
            }


            var order = new Order
            {
                UserId = user.Id,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
                Status = OrderStatus.Pending,
                CreatedAt = DateTime.Now,
                OrderItems = new List<OrderItem>()
            };


            foreach (var item in cart.cartItems)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            order.TotalPrice = order.OrderItems
                .Sum(x => x.Price * x.Quantity);


            await _unitOfWork.Orders.AddAsync(order);


            _unitOfWork.CartItems.RemoveRange(cart.cartItems);

            await _unitOfWork.SaveChangesAsync();


            return RedirectToAction(nameof(Confirmation), new { id = order.Id });
        }


        public async Task<IActionResult> Confirmation(int id)
        {
            var order = await _unitOfWork.Orders.GetBetterVersionAsync(
                o => o.Id == id,
                include: q => q.Include(o => o.OrderItems)
                               .ThenInclude(oi => oi.Product)
            );

            if (order == null)
                return NotFound();

            return View(order);
        }
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);

            var orders = await _unitOfWork.Orders.GetAllBetterVersionAsync(
                o => o.UserId == user.Id,
                include: q => q.Include(o => o.OrderItems)
            );

            return View(orders);
        }

        public async Task<IActionResult> OrderDetails(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var order = await _unitOfWork.Orders.GetBetterVersionAsync(
                o => o.Id == id && o.UserId == user.Id,
                include: q => q
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
            );

            if (order == null)
                return NotFound();

            return View(order);
        }

    }
}