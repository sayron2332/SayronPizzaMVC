using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SayronPizzaMVC.Core.DTO_s.Products;
using SayronPizzaMVC.Core.Services;

namespace SayronPizzaMVC.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(ProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllProducts();
            
            return View(result.Payload);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductDto appProduct)
        {
            return View();
        }
    }
}
