using System.Collections.Generic;
using System.Linq;

namespace SportStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product { Name = "Football", Price = 25m },
            new Product { Name = "Surf board", Price = 179m },
            new Product { Name = "Running Shoes", Price = 95 }
        }.AsQueryable();
    }
}
