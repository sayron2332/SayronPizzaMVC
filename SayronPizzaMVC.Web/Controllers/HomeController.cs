using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayronPizzaMVC.Core.DTO_s;
using SayronPizzaMVC.Core.DTO_s.Products;
using SayronPizzaMVC.Core.Services;
using SayronPizzaMVC.Core.Validation.User;
using SayronPizzaMVC.Web.Models;
using System.Diagnostics;

namespace SayronPizzaMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        private readonly UserService _userService;
        public HomeController(ProductService productService, UserService userService)
        {
            _productService = productService;
            _userService = userService; 
        }

        public async Task<IActionResult> Index()
        {
            List<PizzaDto> result = await _productService.GetAllPizza();
            return View(result);

        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(CreateUserDto model)
        {
            var validator = new CreateUserValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.CreateAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.AuthError = result.Payload;
                return View(model);
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }
        public IActionResult SignIn()
        {
            var user = HttpContext.User.Identity.IsAuthenticated;
            if (user)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(LoginUserDto model)
        {

            LoginUserValidator validator = new LoginUserValidator();
            var validationResult = validator.Validate(model);
            if (validationResult.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.Success)
                {
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Errors = result.Message;
                return View("SignIn");
            }
            ViewBag.Errors = validationResult.Errors[0];
            return View("SignIn");
        }
        public async Task<IActionResult> Logout()
        {
            await _userService.LogoutAsync();
            return RedirectToAction(nameof(Index));
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

        public IActionResult PrintBasket()
        {
            return View();
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
