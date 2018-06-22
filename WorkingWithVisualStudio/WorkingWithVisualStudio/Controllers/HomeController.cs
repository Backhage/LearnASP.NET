using Microsoft.AspNetCore.Mvc;
using WorkingWithVisualStudio.Models;

namespace WorkingWithVisualStudio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(SimpleRepository.SharedRepository.Products);
        }
    }
}