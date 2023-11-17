using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Services;

namespace SayronPizzaMVC.Web.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _categoryService.GetAll());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppCategory category)
        {
            if (category.Name != null)
            {
                await _categoryService.Create(category);
                ViewBag.AuthError = "category successfully add";
                return View(category);
            }
            ViewBag.AuthError = "please enter name";
            return View();
        }

    }
}
