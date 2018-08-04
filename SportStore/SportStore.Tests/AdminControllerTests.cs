using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SportStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" }
            }.AsQueryable());

            var target = new AdminController(mock.Object);

            var result = GetViewModel<IEnumerable<Product>>(target.Index())?.ToArray();

            Assert.Equal(4, result.Length);
            Assert.Equal("P1", result[0].Name);
            Assert.Equal("P2", result[1].Name);
            Assert.Equal("P3", result[2].Name);
            Assert.Equal("P4", result[3].Name);
        }

        [Fact]
        public void Can_Edit_Product()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
            }.AsQueryable());

            var target = new AdminController(mock.Object);

            var p1 = GetViewModel<Product>(target.Edit(1));
            var p2 = GetViewModel<Product>(target.Edit(2));
            var p3 = GetViewModel<Product>(target.Edit(3));

            Assert.Equal(1, p1.ProductID);
            Assert.Equal(2, p2.ProductID);
            Assert.Equal(3, p3.ProductID);
        }

        [Fact]
        public void Cannot_Edit_Nonexistant_Product()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new[]
            {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
            }.AsQueryable());

            var target = new AdminController(mock.Object);
            var result = GetViewModel<Product>(target.Edit(4));

            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            var mock = new Mock<IProductRepository>();
            var tempData = new Mock<ITempDataDictionary>();
            var target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };

            var product = new Product { Name = "Test" };

            var result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Cannot_Save_Invalid_Changes()
        {
            var mock = new Mock<IProductRepository>();
            var target = new AdminController(mock.Object);
            target.ModelState.AddModelError("error", "error");

            var product = new Product { Name = "Test" };

            var result = target.Edit(product);

            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsType<ViewResult>(result);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
