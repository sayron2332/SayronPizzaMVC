using Microsoft.AspNetCore.Mvc;
using SayronPizzaMVC.Core.DTO_s.Products;
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
            List<PizzaDto> result = await _productService.GetAllPizza();
            return View(result);

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
