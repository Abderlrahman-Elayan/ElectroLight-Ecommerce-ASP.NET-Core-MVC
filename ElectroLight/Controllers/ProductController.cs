using ElectroLight.Application.Services.Implementation;
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
        private readonly IImageService _imageService;

        public ProductController(IProductService productService,
                           ICategoryService categoryService, IImageService imageService
                          )
        {
            _productService = productService;
            _categoryService = categoryService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            //var products = await _productService.GetAllAsync(includes: p=>p.Category);
            //return View(products);
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            Product product = await _productService.GetAsync(p => p.Id == id, AsTracking: false, includes: p => p.Category) ?? new();

            string ImagePath = _imageService.GetImageFullPath(product.ImageUrl);

            if (!System.IO.File.Exists(ImagePath))
            {
                product.ImageUrl = "/img/placeholder.jpg";
            }

            ProductVM productVM = new ProductVM()
            {
                Product = product,
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

            productvm.CategoriesList = await getCategoriesListItemsAsync();


            if (!ModelState.IsValid)
            {
                return View(productvm);
            }

            if (productvm.Product.Image != null)
            {
                try
                {
                    productvm.Product.ImageUrl = await _imageService.UploadAndNormalizeImageAsync(productvm.Product.Image, productvm.Product.ImageUrl,"ProductImages");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Product.Image", "An error occurred while uploading the image. Please make sure you uploaded imgae file like: .jpg,.jpeg,.png,.webp,.gif");
                    return View(productvm);
                }
            }
            else
            {
                string ImagePath = _imageService.GetImageFullPath(productvm.Product.ImageUrl);

                if (!System.IO.File.Exists(ImagePath))
                    productvm.Product.ImageUrl = "/img/placeholder.jpg";
            }

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

        public async Task<IActionResult> ProductsShow(int? categoryId)
        {
            ProductShowVM productVM = new()
            {
                CategoriesList = await _categoryService.GetAllAsync(),
                CategoryName = (await _categoryService.GetAsync(c => c.Id == categoryId))?.Name,
                ProductsList = await _productService.GetAllAsync(p => p.CategoryId == categoryId)
            };
            return View(productVM);
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

            _imageService.DeleteImage(Product.ImageUrl);

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
