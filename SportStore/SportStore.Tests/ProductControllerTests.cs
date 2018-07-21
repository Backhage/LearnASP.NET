using Microsoft.AspNetCore.Mvc;
using Moq;
using SportStore.Controllers;
using SportStore.Models;
using SportStore.Models.ViewModels;
using System;
using System.Linq;
using Xunit;

namespace SportStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(
                (new[]
                {
                    new Product { ProductID = 1, Name = "P1" },
                    new Product { ProductID = 2, Name = "P2" },
                    new Product { ProductID = 3, Name = "P3" },
                    new Product { ProductID = 4, Name = "P4" },
                    new Product { ProductID = 5, Name = "P5" }
                }).AsQueryable());

            var controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            var result = controller.List(null, 2).ViewData.Model as ProductsListViewModel; 

            var products = result.Products.ToArray();
            Assert.Equal(2, products.Length);
            Assert.Equal("P4", products[0].Name);
            Assert.Equal("P5", products[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product { ProductID = 1, Name = "P1" },
                new Product { ProductID = 2, Name = "P2" },
                new Product { ProductID = 3, Name = "P3" },
                new Product { ProductID = 4, Name = "P4" },
                new Product { ProductID = 5, Name = "P5" },
            }).AsQueryable());

            var controller = new ProductController(mock.Object) { PageSize = 3 };

            var result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;

            var pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new []
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product {ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product {ProductID = 3, Name = "P3", Category = "Cat1" },
                new Product {ProductID = 4, Name = "P4", Category = "Cat2" },
                new Product {ProductID = 5, Name = "P5", Category = "Cat3" },
            }).AsQueryable);

            var controller = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            var result = (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();

            Assert.Equal(2, result.Length);
            Assert.Equal("P2", result[0].Name);
            Assert.Equal("Cat2", result[0].Category);
            Assert.Equal("P4", result[1].Name);
            Assert.Equal("Cat2", result[1].Category);
        }

        [Fact]
        public void Generates_Category_Specific_Product_Count()
        {
            var mock = new Mock<IProductRepository>();
            mock.SetupGet(m => m.Products).Returns((new[]
            {
                new Product { ProductID = 1, Name = "P1", Category = "Cat1" },
                new Product { ProductID = 2, Name = "P2", Category = "Cat2" },
                new Product { ProductID = 3, Name = "P3", Category = "Cat1" },
                new Product { ProductID = 4, Name = "P4", Category = "Cat2" },
                new Product { ProductID = 5, Name = "P5", Category = "Cat3" },
            }).AsQueryable());

            var target = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            ProductsListViewModel GetModel(ViewResult result) => result?.ViewData?.Model as ProductsListViewModel;

            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
