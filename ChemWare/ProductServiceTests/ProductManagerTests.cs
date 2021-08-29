using Moq;
using NUnit.Framework;
using ProductService.BusinessLayer;
using ProductService.DataAccess;
using ProductService.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Tests
{
    public class ProductManagerTests
    {
        private Mock<IProductDataSource> mockProductSource;
        private Mock<IProductTypeDataSource> mockProductTypeSource;

        [SetUp]
        public void Setup()
        {
            mockProductSource = new Mock<IProductDataSource>();

            List<Product> inMemDb = new List<Product>();
            inMemDb.Add(new Product() { ProductId = 1, ProductTypeId = 1, Name = "apple", Price = 12.12415f, Active = true, IsDeleted = false  });
            inMemDb.Add(new Product() { ProductId = 2, ProductTypeId = 2, Name = "orange", Price = 23.15732f, Active = true, IsDeleted = false });
            inMemDb.Add(new Product() { ProductId = 3, ProductTypeId = 2, Name = "banana", Price = 5.14571f, Active = true, IsDeleted = false });

            List<int> callArgs = new List<int>();

            mockProductSource
                .Setup(t => t.GetProduct(It.IsAny<int>()))
                .Returns<int>(i => inMemDb.Where(x => x.ProductId == i).FirstOrDefault());

            mockProductTypeSource = new Mock<IProductTypeDataSource>();
            mockProductTypeSource.Setup(t => t.GetProductType(It.IsAny<int>())).Returns(new ProductType() {Code = "testType",  Description = "testType", Name = "testType", ProductTypeId = 1 });
        }

        [Test]
        public void GetProductByIdReturnsCorrectProduct()
        {
            var productManager = new ProductManager(mockProductSource.Object, mockProductTypeSource.Object);

            int id = 1;
            var prod = productManager.GetProductDetails(id);
            Assert.IsTrue(id == prod.ProductId);

            id = 2;
            prod = productManager.GetProductDetails(id);
            Assert.IsTrue(id == prod.ProductId);

            id = 3;
            prod = productManager.GetProductDetails(id);
            Assert.IsTrue(id == prod.ProductId);
        }

        [Test]
        public void GetProductByIdReturnsProductTypeName()
        {
            var productManager = new ProductManager(mockProductSource.Object, mockProductTypeSource.Object);

            int id = 2;
            var prod = productManager.GetProductDetails(id);
            Assert.IsTrue(id == prod.ProductId);

            Assert.AreEqual("testType", prod.ProductTypeName);
        }

        [Test]
        public void GetProductByIdFormatsPriceTo2DecimalPlaces()
        {
            var productManager = new ProductManager(mockProductSource.Object, mockProductTypeSource.Object);

            int id = 2;
            var prod = productManager.GetProductDetails(id);
            Assert.IsTrue(id == prod.ProductId);

            Assert.AreEqual("23.16", prod.Price.ToString());
        }
    }
}