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

        
    }
}
