using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SayronPizzaMVC.Core.DTO_s.Categories;
using SayronPizzaMVC.Core.DTO_s.Products;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Services;
using SayronPizzaMVC.Core.Validation.Category;
using X.PagedList;

namespace SayronPizzaMVC.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;
        private readonly ProductService _productService;
        public CategoryController(CategoryService categoryService, ProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
          
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
        public async Task<IActionResult> Create(CategoryDto category)
        {
            var validator = new CreateCategoryValidation();
            var validateResult = validator.Validate(category);
            if (validateResult.IsValid)
            {
                await _categoryService.Create(category);
                ViewBag.AuthError = "category successfully add";
                return View(category);
            }
            ViewBag.AuthError = "please enter name";
            return View();
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var categoryDto = await _categoryService.Get(id);

            if (categoryDto == null)
            {
                ViewBag.AuthError = "Category not found.";
                return View();
            }
            return View(categoryDto);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDto model) // 1-FromForm, 2-FromRoute, 
        {
            var validator = new CreateCategoryValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                await _categoryService.Update(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthError = validationResult.Errors[0];
            return View(model);
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var categoryDto = await _categoryService.Get(id);

            if (categoryDto == null)
            {
                ViewBag.AuthError = "Category not found.";
                return View();
            }

            List<ProductDto> posts = await _productService.GetByCategory(id);
            ViewBag.CategoryName = categoryDto.Name;
            ViewBag.CategoryId = categoryDto.Id;

            int pageSize = 20;
            int pageNumber = 1;
            return View("Delete", posts.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var categoryDto = await _categoryService.Get(id);
            if (categoryDto == null)
            {
                ViewBag.AuthError = "Category not found.";
                return View();
            }
            await _categoryService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
