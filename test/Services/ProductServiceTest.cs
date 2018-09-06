using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using WebApi.Entities;
using WebApi.Test.SampleData;
using WebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Test.Services
{
    [TestClass]
    public class ProductServiceTest
    {
        private List<Product> products;

        [TestInitialize]
        public void Init()
        {
            products = TestData.GetData().ToList();
        }

        [TestMethod()]
        public void GetProducts_returns_all_products_without_filter()
        {
            //given
            int countFilter = 5;
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new DataContext(options);
            db.Products.AddRange(products);
            db.SaveChanges();
            var sut = new ProductService(db);

            //when
            var productList = sut.GetProducts();

            //then
            Assert.IsInstanceOfType(productList, typeof(List<Product>));
            Assert.AreEqual(countFilter, productList.Count);
        }

        [TestMethod()]
        public void GetProducts_returns_all_products_with_filter()
        {
            //given
            int countFilter = 5;
            products.AddRange(TestData.GetData(countFilter, "apple"));
            var options = new DbContextOptionsBuilder<DataContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var db = new DataContext(options);
            db.Products.AddRange(products);
            db.SaveChanges();
            var sut = new ProductService(db);

            //when
            var productList = sut.GetProducts("apple");

            //then
            Assert.IsInstanceOfType(productList, typeof(List<Product>));
            Assert.AreEqual(countFilter, productList.Count);
        }
    }
}