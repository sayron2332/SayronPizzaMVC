using Microsoft.AspNetCore.Mvc;
using SayronPizzaMVC.Core.DTO_s.Products;


namespace SayronPizzaMVC.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
