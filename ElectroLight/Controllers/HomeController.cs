using ElectroLight.Application.Services.IServices;
using ElectroLight.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ElectroLight.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;

        public HomeController(IProductService productService)
        {
            this.productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productService.GetAllAsync(includes: p => p.Category);
            return View(products);
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
