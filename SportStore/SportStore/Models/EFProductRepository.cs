using System.Linq;

namespace SportStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products; 

        public void SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var dbEntry = _context.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (dbEntry != null)
                {
                    (dbEntry.Name, dbEntry.Description, dbEntry.Price, dbEntry.Category) =
                        (product.Name, product.Description, product.Price, product.Category);
                }
            }
            _context.SaveChanges();
        }
    }
}
