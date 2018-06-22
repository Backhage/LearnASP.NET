using Microsoft.AspNetCore.Mvc;
using Razor.Models;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var products = new[]
            {
                new Product {Name = "Kayak", Price = 275m},
                new Product {Name = "Lifejacket", Price = 48.95m},
                new Product {Name = "Soccer ball", Price = 19.50m},
                new Product {Name = "Corner flag", Price = 34.95m}
            };
            return View(products);
        }
    }
}