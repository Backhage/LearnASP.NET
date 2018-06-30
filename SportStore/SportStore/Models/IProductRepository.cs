using System.Linq;

namespace SportStore.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
