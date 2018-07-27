using Microsoft.AspNetCore.Mvc;
using SportStore.Models;

namespace SportStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Checkout()
        {
            return View(new Order());
        }
    }
}