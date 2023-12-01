using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SayronPizzaMVC.Core.DTO_s.Products;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Services;
using SayronPizzaMVC.Core.Validation.Product;
using X.PagedList;

namespace SayronPizzaMVC.Web.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
        
        private readonly ProductService _productService;
        private readonly IMapper _mapper;
        private readonly CategoryService _categoryService;

        public ProductController(ProductService productService, IMapper mapper, CategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int? page)
        {
            List<ProductDto> products = await _productService.GetAllProducts();
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(products.ToPagedList(pageNumber, pageSize));
        }

        public async Task<IActionResult> Create()
        {
            await LoadCategories();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDto model)
        {
            var validator = new CreateProductValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                model.File = files;
                await _productService.Create(model);
                return RedirectToAction("Index", "Product");
            }

            ViewBag.AuthError = validationResult.Errors[0];
            await LoadCategories();
            return View();
        }
      
        public async Task<IActionResult> Edit(int id)
        {
            var products = await _productService.GetById(id);

            if (products == null) return NotFound();

            await LoadCategories();
            return View(products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDto model)
        {
            var validator = new CreateProductValidation();
            var validationResult = await validator.ValidateAsync(model);
            if (validationResult.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                model.File = files;
                await _productService.Update(model);
                return RedirectToAction("Index", "Product");
            }
            ViewBag.CreateProductError = validationResult.Errors[0];
            await LoadCategories();
            return View(model);
        }

        public async Task<IActionResult> Search([FromForm] string searchString)
        {

            List<ProductDto> products = await _productService.Search(searchString);
            if (searchString == null)
            {
                products = await _productService.GetAllProducts();
            }
            int pageSize = 20;
            int pageNumber = 1;
            return View("Index", products.ToPagedList(pageNumber, pageSize));
        }

        private async Task LoadCategories()
        {
            ViewBag.CategoryList = new SelectList(
                await _categoryService.GetAll(),
                nameof(AppCategory.Id),
                nameof(AppCategory.Name)
                );
            ;
        }
    }
}
