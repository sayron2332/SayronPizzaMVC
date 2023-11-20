using Microsoft.AspNetCore.Mvc;
using SayronPizzaMVC.Core.Services;
using SayronPizzaMVC.Web.Models;
using System.Diagnostics;

namespace SayronPizzaMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        public HomeController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllPizza();
            return View(result.Payload);

        }
        public IActionResult PrintDrinks() { 
            return View();
        }

        public IActionResult PrintSides()
        {
            return View();
        }

        public IActionResult PrintSalads()
        {
            return View();
        }

        public IActionResult PrintDesserts()
        {
            return View();
        }

    }
}
