using Microsoft.AspNetCore.Mvc;
using SayronPizzaMVC.Web.Models;
using System.Diagnostics;

namespace SayronPizzaMVC.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
