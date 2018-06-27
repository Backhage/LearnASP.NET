using WorkingWithVisualStudio.Models;
using Xunit;

namespace WorkingWithVisualStudio.Tests
{
    public class ProductTests
    {
        [Fact]
        public void CanChangeProductName()
        {
            var p = new Product { Name = "Test", Price = 100m };
            p.Name = "New Name";
            Assert.Equal("New Name", p.Name); 
        }

        [Fact]
        public void CanChangeProductPrice()
        {
            var p = new Product { Name = "Test", Price = 100m };
            p.Price = 200m;
            Assert.Equal(200m, p.Price);
        }
    }
}
