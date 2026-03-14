using ElectroLight.Application.Services.IServices;
using ElectroLight.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ElectroLight.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Product());
            }

            var obj = await _service.GetAsync(c => c.Id == id);

            if (obj == null)
                return RedirectToAction("Error", "Home");

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Product product)
        {
            if (product.Name == product.Description)
                ModelState.AddModelError("description", "The description cannot exactly match the Name.");

            if (!ModelState.IsValid)
                return View(product);

            if (product.Id == 0)
            {
                await _service.AddAsync(product);
                TempData["success"] = "The Product has been Created successfully.";
            }
            else
            {
                await _service.UpdateAsync(product);
                TempData["success"] = "The Product has been Updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }



        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Product> ProductList = (await _service.GetAllAsync()).ToList();
            return Json(new { data = ProductList });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var Product = await _service.GetAsync(c => c.Id == id);
            if (Product == null)
            {
                TempData["error"] = "cant delete Product";
                return Json(new { success = false, message = "Error while deleting" });

            }

            await _service.DeleteAsync(Product);
            TempData["success"] = "The Product has been Deleted successfully.";

            return Json(new { success = true, message = "Product has been Deleted Successfuly" });
        }

        #endregion


    }
}
