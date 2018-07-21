using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System.Linq;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize { get; set; } = 4;

        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(string category, int page = 1)
        {
            var products = _repository.Products
                .Where(p => category == null || p.Category.Equals(category))
                .OrderBy(p => p.ProductID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize);

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = PageSize,
                TotalItems = _repository.Products.Count()
            };

            var productsListViewModel = new ProductsListViewModel
            {
                Products = products,
                PagingInfo = pagingInfo,
                CurrentCategory = category
            };

            return View(productsListViewModel);
        }
    }
}
