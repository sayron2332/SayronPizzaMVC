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

        public async Task<IActionResult> PrintSides()
        {
            List<SidesDto> result = await _productService.GetAllSides();
            return View(result);
        }
        public async Task<IActionResult> PrintSalads()
        {
            List<SaladsDto> result = await _productService.GetAllSalads();
            return View(result);
        }
        public async Task<IActionResult> PrintDesserts()
        {
            List<DessertsDto> result = await _productService.GetAllDesserts();
            return View(result);
        }
        public async Task<IActionResult> PrintDrinks()
        {
            List<DrinkDto> result = await _productService.GetAllDrinks();
            return View(result);
        }




    }
}
