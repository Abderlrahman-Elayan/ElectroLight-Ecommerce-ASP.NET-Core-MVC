using ElectroLight.Application.Common.Interfaces;
using ElectroLight.Domain.Entities;
using ElectroLight.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace ElectroLight.Controllers
{
    public class CategoryController : Controller
    {
        private IUnitOfWork _UnitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _UnitOfWork.Categories.GetAllAsync();
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

            await _UnitOfWork.Categories.AddAsync(category);
            await _UnitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int categoryId)
        {

            var obj = await _UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (obj == null) return NotFound();



            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid) return View(category);


            _UnitOfWork.Categories.Update(category);
            await _UnitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int categoryId)
        {

            var obj = await _UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);

            if (obj == null) return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category category)
        {
            _UnitOfWork.Categories.Remove(category);
            await _UnitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
