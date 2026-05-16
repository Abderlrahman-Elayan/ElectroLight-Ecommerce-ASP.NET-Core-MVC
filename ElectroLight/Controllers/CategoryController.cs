    using ElectroLight.Application.Interfaces;
using ElectroLight.Application.Services.Implementation;
using ElectroLight.Application.Services.IServices;
using ElectroLight.Application.Utilies;
using ElectroLight.Domain.Entities;
using ElectroLight.Infrastructure.Data;
using ElectroLight.Infrastructure.Services;
using ElectroLight.ViewsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectroLight.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _CategoryService;
        private readonly IProductService _ProductService;
        private readonly IImageService _imageService;

        public CategoryController(ICategoryService service, IImageService imageService, IProductService productService)
        {
            _CategoryService = service;
            _imageService = imageService;
            _ProductService = productService;
        }

        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Index()
        {
            //var categories = await _service.GetAllAsync();
            //return View(categories);
            return View();
        }

        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Upsert(int? id)
        {

            Category category = await _CategoryService.GetAsync(p => p.Id == id, AsTracking: false) ?? new();

            string ImagePath = _imageService.GetImageFullPath(category.ImageUrl);

            if (!System.IO.File.Exists(ImagePath))
            {
                category.ImageUrl = "/img/placeholder.jpg";
            }

            return View(category);
        }

        [Authorize(Roles = SD.Role_Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (category.Name == category.Description)
                ModelState.AddModelError("description", "The description cannot exactly match the Name.");

            var isNameExist = await _CategoryService.GetAsync(c=> c.Name.Trim().ToLower() == category.Name.Trim().ToLower(), AsTracking: false);

            if (isNameExist != null)
                ModelState.AddModelError("name", "The name is already existing");


            if (!ModelState.IsValid)
                return View(category);

            if (category.Image != null)
            {
                try
                {
                    category.ImageUrl = await _imageService.UploadAndNormalizeImageAsync(category.Image, category.ImageUrl,"CategoryImages");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Product.Image", "An error occurred while uploading the image. Please make sure you uploaded imgae file like: .jpg,.jpeg,.png,.webp,.gif");
                    return View(category);
                }
            }
            else
            {
                string ImagePath = _imageService.GetImageFullPath(category.ImageUrl);

                if (!System.IO.File.Exists(ImagePath))
                    category.ImageUrl = "/img/placeholder.jpg";
            }


            if (category.Id == 0)
            {
                await _CategoryService.AddAsync(category);
                TempData["success"] = "The Category has been Created successfully.";
            }
            else
            {
                await _CategoryService.UpdateAsync(category);
                TempData["success"] = "The Category has been Updated successfully.";
            }   

            return RedirectToAction(nameof(Index));
        }

       

        #region API CALLS
        [HttpGet]
        [Authorize(Roles = SD.Role_Admin)]

        public async Task<IActionResult> GetAll()
        {
            List<Category> CategoryList = (await _CategoryService.GetAllAsync()).ToList();
            return Json(new { data = CategoryList });
        }


        [HttpDelete]
        [Authorize(Roles = SD.Role_Admin)]
        public async Task<IActionResult> Delete(int? id)
        {
            var Category = await _CategoryService.GetAsync(c => c.Id == id);

          

            if (Category == null)
            {
                TempData["error"] = "cant delete Category";
                return Json(new { success = false, message = "Error while deleting" });

            }

            var product = await _ProductService.GetAsync(p => p.CategoryId == id);

            if (product != null)
            {
                return Json(new
                {
                    success = false,
                    message = "Cannot delete this category because it contains products."
                });
            }

            _imageService.DeleteImage(Category.ImageUrl);

            await _CategoryService.DeleteAsync(Category);
            TempData["success"] = "The Category has been Deleted successfully.";

            return Json(new { success = true, message = "Category has been Deleted Successfuly" });
        }
        #endregion



    }
}
