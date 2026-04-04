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
        private readonly IProductService productService;

        public HomeController(ICategoryService categoryService,IProductService productService)
        {
            this.categoryService = categoryService;
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM homeVM = new()
            {
                categoriesList = await categoryService.GetAllAsync(),
                newestProductsList = (await productService.GetNewestProductsAsync(18)),
                featuredProductsList = (await productService.GetFeaturedProductsAsync())

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

