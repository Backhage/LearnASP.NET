using SportStore.Models;
using System.Linq;
using Xunit;

namespace SportStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            var results = target.Lines.ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);

            var results = target.Lines.OrderBy(c => c.Product.ProductID).ToArray();

            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            var p1 = new Product { ProductID = 1, Name = "P1" };
            var p2 = new Product { ProductID = 2, Name = "P2" };
            var p3 = new Product { ProductID = 3, Name = "P3" };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);

            target.RemoveLine(p2);

            Assert.Equal(0, target.Lines.Count(c => c.Product == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            var p1 = new Product { ProductID = 1, Name = "P1", Price = 100m };
            var p2 = new Product { ProductID = 2, Name = "P2", Price = 50m };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            var result = target.ComputeTotalValue();

            Assert.Equal(450m, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            var p1 = new Product { ProductID = 1, Name = "P1", Price = 100m };
            var p2 = new Product { ProductID = 2, Name = "P2", Price = 50m };

            var target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            target.Clear();

            Assert.Empty(target.Lines);
        }
    }
}
