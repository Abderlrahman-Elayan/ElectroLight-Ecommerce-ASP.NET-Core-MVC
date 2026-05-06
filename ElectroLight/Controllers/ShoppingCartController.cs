using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Application.Services.IServices;
using ElectroLight.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
namespace ElectroLight.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;

        public ShoppingCartController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IProductService productService)
        {
            this._unitOfWork = unitOfWork;
            this._userManager = userManager;
            this._productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var shoppingCart = await GetShoppingCartAsync();

            return View(shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateItem(int productId, int quantity = 1)
        {
            if (quantity <= 0)
                return BadRequest();

            var product = await _productService.GetAsync(p => p.Id == productId);
            if (product == null)
                return NotFound();

            var shoppingCart = await GetShoppingCartAsync();

            var cartItem = await _unitOfWork.CartItems.GetAsync(
                c => c.ShoppingCartId == shoppingCart.Id &&
                     c.ProductId == productId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    ShoppingCartId = shoppingCart.Id,
                    Price = product.Price
                };
                await _unitOfWork.CartItems.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
                _unitOfWork.CartItems.Update(cartItem);
            }

            await _unitOfWork.SaveChangesAsync();

            var countItems = (await _unitOfWork.CartItems.GetAllAsync(c => c.ShoppingCartId == shoppingCart.Id)).Count();


            return Ok(new { success = true, count = countItems});
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            var item = await _unitOfWork.CartItems.GetAsync(c => c.Id == cartItemId);

            if (item == null)
                return NotFound();

            item.Quantity = quantity;

            _unitOfWork.CartItems.Update(item);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { success = true });
        }


        [HttpGet]
        public async Task<IActionResult> GetCartCount()
        {
            var shoppingCart = await GetShoppingCartAsync();

            var countItems = (await _unitOfWork.CartItems.GetAllAsync(c => c.ShoppingCartId == shoppingCart.Id)).Count();


            return Ok(new { count = countItems });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCartItem(int cartItemId)
        {
            var cartItem = await _unitOfWork.CartItems.GetAsync(c => c.Id == cartItemId);

            if (cartItem == null)
                return NotFound();

            _unitOfWork.CartItems.Remove(cartItem);
            await _unitOfWork.SaveChangesAsync();


            return Json(new { success = true });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAllCartItems(int ShoppingCartId)
        {
            var shoppingCart = await _unitOfWork.ShoppingCarts.GetAsync(sc => sc.Id == ShoppingCartId);

            if (shoppingCart == null)
            {
                return NotFound();
            }
            
            var cartItems = await _unitOfWork.CartItems.GetAllAsync(ci => ci.ShoppingCartId == shoppingCart.Id);

            if( cartItems == null)
            {
                return NotFound();
            }

            _unitOfWork.CartItems.RemoveRange(cartItems);

            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true });
        }

        private async Task<ShoppingCart> GetShoppingCartAsync()
        {

            var user = await _userManager.GetUserAsync(User);

            var shoppingCart = await _unitOfWork.ShoppingCarts.GetBetterVersionAsync(
                c => c.UserId == user.Id,
                AsTracking: true,
                include: q => q
                    .Include(c => c.User)
                    .Include(c => c.cartItems)
                        .ThenInclude(ci => ci.Product)
            );

            if (shoppingCart == null)
            {
                shoppingCart = new()
                {
                    UserId = user.Id,
                };

                await _unitOfWork.ShoppingCarts.AddAsync(shoppingCart);
                await _unitOfWork.SaveChangesAsync();
            }

            return shoppingCart;
        }
    }
}
