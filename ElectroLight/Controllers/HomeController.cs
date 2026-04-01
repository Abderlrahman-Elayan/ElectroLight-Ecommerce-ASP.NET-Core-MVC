using ElectroLight.Application.Services.IServices;
using ElectroLight.Models;
using ElectroLight.ViewsModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElectroLight.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService categoryService;

        public HomeController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new()
            {
                categoriesList = await categoryService.GetAllAsync(),
            };
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

