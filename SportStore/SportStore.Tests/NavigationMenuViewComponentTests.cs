using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportStore.Components;
using SportStore.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            var mock = new Mock<IProductRepository>();
            mock.SetupGet(m => m.Products).Returns((new[]
            {
                new Product { ProductID = 1, Name = "P1", Category = "Apples" },
                new Product { ProductID = 2, Name = "P2", Category = "Apples" },
                new Product { ProductID = 3, Name = "P3", Category = "Plums" },
                new Product { ProductID = 4, Name = "P4", Category = "Oranges" }
            }).AsQueryable());

            var target = new NavigationMenuViewComponent(mock.Object);

            var results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
        }
    }
}
