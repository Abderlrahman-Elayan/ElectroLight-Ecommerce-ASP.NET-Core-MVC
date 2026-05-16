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
                newestProductsList = (await productService.GetNewestProductsAsync(0,11)),
                featuredProductsList = (await productService.GetFeaturedProductsAsync(0,11))

            };
            return View(homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMoreFeaturedProducts(int skip, int take = 5)
        {
            var products = await productService
                .GetFeaturedProductsAsync(skip, take);

            return PartialView("_ProductCardsPartial", products);
        }

        [HttpGet]
        public async Task<IActionResult> LoadMoreNewestProducts(int skip, int take = 5)
        {
            var products = await productService
                .GetNewestProductsAsync(skip, take);

            return PartialView("_ProductCardsPartial", products);
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

