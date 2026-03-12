using ElectroLight.Application.Interfaces;
using ElectroLight.Application.Interfaces.IServices;
using ElectroLight.Domain.Entities;
using ElectroLight.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElectroLight.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _service.GetAllAsync();
            return View(categories);
        }
        public IActionResult Create()
        {
            var obj = new Category();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            await _service.AddAsync(category);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int categoryId)
        {

            var obj = await _service.GetAsync(c => c.Id == categoryId);
            if (obj == null) return NotFound();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid) return View(category);

            await _service.UpdateAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int categoryId)
        {
            var obj = await _service.GetAsync(c => c.Id == categoryId);

            if (obj == null)
                return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            await _service.DeleteAsync(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
