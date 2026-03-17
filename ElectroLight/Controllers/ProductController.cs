using ElectroLight.Application.Services.IServices;
using ElectroLight.Domain.Entities;
using ElectroLight.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ElectroLight.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IProductService productService,
                           ICategoryService categoryService,
                           IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()

        {
            var products = await _productService.GetAllAsync(includes: p=>p.Category);
            return View(products);
        }

        public async Task<IActionResult> Upsert(int? id)
        {

            ProductVM productVM = new ProductVM()
            {
                Product = await _productService.GetAsync(p => p.Id == id) ?? new Product(),

                CategoriesList = await getCategoriesListItemsAsync()
            };

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductVM productvm)
        {
            if (productvm.Product.Name == productvm.Product.Description)
                ModelState.AddModelError("description", "The description cannot exactly match the Name.");

            if (!ModelState.IsValid)
            {
                productvm.CategoriesList = await getCategoriesListItemsAsync();

                return View(productvm);
            }

            //NOTE:
            //image validation should send to the service later
            //////////////////////////////////////////////////////
            //////////////////////////////////////////////////////
            if (productvm.Product.Image != null)
            {
                var ext = Path.GetExtension(productvm.Product.Image.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
                if (!allowedExtensions.Contains(ext))
                {
                    ModelState.AddModelError("Product.Image", "Only image files are allowed.");
                    productvm.CategoriesList = await getCategoriesListItemsAsync();
                    return View(productvm);
                }

                string fileName = $"{Guid.NewGuid()}{ext}";

                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", "ProductImages");

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fullPath = Path.Combine(folderPath, fileName);

                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await productvm.Product.Image.CopyToAsync(fileStream);
                }

                productvm.Product.ImageUrl = $"/img/ProductImages/{fileName}";
            }
            else
            {
                productvm.Product.ImageUrl = "/img/placeholder.jpg";
            }
            //////////////////////////////////////////////////////
            //////////////////////////////////////////////////////

            if (productvm.Product.Id == 0)
            {
                await _productService.AddAsync(productvm.Product);
                TempData["success"] = "The Product has been Created successfully.";
            }
            else
            {
                await _productService.UpdateAsync(productvm.Product);
                TempData["success"] = "The Product has been Updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }


        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Product> ProductList = (await _productService.GetAllAsync()).ToList();
            return Json(new { data = ProductList });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var Product = await _productService.GetAsync(c => c.Id == id);
            if (Product == null)
            {
                TempData["error"] = "cant delete Product";
                return Json(new { success = false, message = "Error while deleting" });

            }

            await _productService.DeleteAsync(Product);
            TempData["success"] = "The Product has been Deleted successfully.";

            return Json(new { success = true, message = "Product has been Deleted Successfuly" });
        }

        #endregion


        private async Task<IEnumerable<SelectListItem>> getCategoriesListItemsAsync()
        {
            return (await _categoryService.GetAllAsync()).Select(c =>
                 new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
        }

    }
}
