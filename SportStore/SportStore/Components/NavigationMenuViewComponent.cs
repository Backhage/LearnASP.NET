using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System.Linq;

namespace SportStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IProductRepository _repository;

        public NavigationMenuViewComponent(IProductRepository repository)
        {
            _repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            return View(_repository.Products.Select(x => x.Category).Distinct().OrderBy(x => x));
        }
    }
}
