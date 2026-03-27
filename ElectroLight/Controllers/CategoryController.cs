using ElectroLight.Application.Interfaces;
using ElectroLight.Application.Services.IServices;
using ElectroLight.Domain.Entities;
using ElectroLight.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElectroLight.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            //var categories = await _service.GetAllAsync();
            //return View(categories);
            return View();
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new Category());
            }

            var obj = await _service.GetAsync(c => c.Id == id);

            if (obj == null)
                return RedirectToAction("Error", "Home");

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Category category)
        {
            if (category.Name == category.Description)
                ModelState.AddModelError("description", "The description cannot exactly match the Name.");

            if (!ModelState.IsValid)
                return View(category);

            if (category.Id == 0)
            {
                await _service.AddAsync(category);
                TempData["success"] = "The Category has been Created successfully.";
            }
            else
            {
                await _service.UpdateAsync(category);
                TempData["success"] = "The Category has been Updated successfully.";
            }

            return RedirectToAction(nameof(Index));
        }

       

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Category> CategoryList = (await _service.GetAllAsync()).ToList();
            return Json(new { data = CategoryList });
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var Category = await _service.GetAsync(c => c.Id == id);
            if (Category == null)
            {
                TempData["error"] = "cant delete Category";
                return Json(new { success = false, message = "Error while deleting" });

            }

            await _service.DeleteAsync(Category);
            TempData["success"] = "The Category has been Deleted successfully.";

            return Json(new { success = true, message = "Category has been Deleted Successfuly" });
        }

        #endregion



    }
}
