using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System.Linq;

namespace SportStore.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View(_repository.Products);
        }

        public IActionResult Edit(int productId)
        {
            return View(_repository.Products.FirstOrDefault(p => p.ProductID == productId));
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            else // There is something wrong with the values
            {
                return View(product);
            }
        }

        public IActionResult Create()
        {
            return View("Edit", new Product());
        }
    }
}