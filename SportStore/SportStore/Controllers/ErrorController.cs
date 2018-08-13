using Microsoft.AspNetCore.Mvc;

namespace SportStore.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
