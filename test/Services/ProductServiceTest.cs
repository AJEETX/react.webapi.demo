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
        DataContext Db;

        [TestInitialize]
        public void Init()
        {
            products = TestData.GetData().ToList();
            var options = new DbContextOptionsBuilder<DataContext>().EnableSensitiveDataLogging(true).UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            Db = new DataContext(options);            
        }
        [TestCleanup]
        public void Clean()
        {
            products=null;
            Db=null;
        }

        [TestMethod()]
        public void GetProducts_returns_all_products_without_filter()
        {
            //given
            int countFilter = 5;

            Db.Products.AddRange(products);
            Db.SaveChanges();
            var sut = new ProductService(Db);

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
            Db.Products.AddRange(products);
            Db.SaveChanges();
            var sut = new ProductService(Db);

            //when
            var productList = sut.GetProducts("apple");

            //then
            Assert.IsInstanceOfType(productList, typeof(List<Product>));
            Assert.AreEqual(countFilter, productList.Count);
        }
    }
}