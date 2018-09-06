using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using WebApi.Entities;
using WebApi.Services;
using WebApi.Test.SampleData;

namespace WebApi.Test
{

[TestClass()]
public class ProductsControllerTests
{
    private Mock<IProductService> moqDbService;
    private List<Product> products;

    [TestInitialize]
    public void Init()
    {
        moqDbService = new Mock<IProductService>();
        products = TestData.GetData().ToList();
    }

    [TestCleanup]
    public void Cleanup()
    {
        moqDbService = null;
        products = null;
    }

    [TestMethod()]
    public void GetAll_return_collection_of_all_products()
    {
        //given
        moqDbService.Setup(m => m.GetProducts(It.IsAny<string>())).Returns(products);
        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.GetProducts();
        var contentResult = actionResult as OkObjectResult;

        //then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(200, contentResult.StatusCode);
    }

    [TestMethod()]
    public void GetAll_return_collection_of_filtered_products_with_filter()
    {
        //given
        string filter = "apple";
        products = TestData.GetData(2, filter).ToList();

        moqDbService.Setup(m => m.GetProducts(It.IsRegex(filter))).Returns(products);
        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.GetProducts(filter);
        var contentResult = actionResult as OkObjectResult;
        IEnumerable<Product> productsResult = (IEnumerable<Product>)contentResult.Value;
        //then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(200, contentResult.StatusCode);
        Assert.IsNotNull(productsResult);
        Assert.AreEqual(products.Count, productsResult.Count());
    }

    [TestMethod()]
    public void GetProduct_returns_product_successful()
    {
        //given
        int id = 1;
        Product product = products.FirstOrDefault();
        moqDbService.Setup(m => m.GetProduct(It.IsAny<int>())).Returns(product);

        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.GetProduct(id);
        var contentResult = actionResult as OkObjectResult;
        Product productResult = (Product)contentResult.Value;
        //then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(product.Id, productResult.Id);
        Assert.AreEqual(product.Description, productResult.Description);
        Assert.AreEqual(product.Brand, productResult.Brand);
        Assert.AreEqual(product.Model, productResult.Model);
    }

    [TestMethod()]
    public void GetProduct_returns_not_found()
    {
        //given
        Product p = null;
        moqDbService.Setup(m => m.GetProduct(It.IsAny<int>())).Returns(p);

        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.GetProduct(1);
        var contentResult = actionResult as NotFoundResult;

        ////then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(404, contentResult.StatusCode);
    }

    [TestMethod()]
    public void PostProduct_add_product_is_successful()
    {
        //given
        Product product = products.FirstOrDefault();
        moqDbService.Setup(m => m.AddProduct(It.IsAny<Product>())).Returns(product);

        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.PostProduct(product);
        var contentResult = actionResult as OkObjectResult;
        Product productResult = (Product)contentResult.Value;
        //then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(200, contentResult.StatusCode);
        Assert.AreEqual(product.Id, productResult.Id);
        Assert.AreEqual(product.Description, productResult.Description);
        Assert.AreEqual(product.Brand, productResult.Brand);
        Assert.AreEqual(product.Model, productResult.Model);
    }

    [TestMethod()]
    public void PostProduct_add_product_with_invalid_model_state_returns_server_error()
    {
        //given
        Product p = null;
        moqDbService.Setup(m => m.AddProduct(It.IsAny<Product>())).Returns(p);

        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.PostProduct(p);
        var errorResult = actionResult as BadRequestObjectResult;

        ////then
        Assert.IsNotNull(errorResult);
        Assert.AreEqual(400, errorResult.StatusCode);
    }

    [TestMethod()]
    public void PutProduct_is_successful()
    {
        //given
        var product = products.FirstOrDefault();
        int id = product.Id;
        moqDbService.Setup(m => m.UpdateProduct(It.IsAny<Product>())).Returns(true);

        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.PutProduct(id, product);
        var contentResult = actionResult as OkObjectResult;

        ////then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(200, contentResult.StatusCode);
    }

    [TestMethod()]
    public void PutProduct_is_unsuccessful()
    {
        //given
        int id = 1;
        moqDbService.Setup(m => m.UpdateProduct(It.IsAny<Product>())).Returns(false);

        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.PutProduct(id, products.FirstOrDefault());
        var contentResult = actionResult as NotFoundResult;

        ////then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(404, contentResult.StatusCode);
    }

    [TestMethod()]
    public void DeleteProduct_is_successful()
    {
        //given
        int id = 1;
        moqDbService.Setup(m => m.DeleteProduct(It.IsAny<int>())).Returns(true);

        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.DeleteProduct(id);
        var contentResult = actionResult as OkObjectResult;

        ////then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(200, contentResult.StatusCode);
        Assert.AreEqual("Product deleted", contentResult.Value);
    }

    [TestMethod()]
    public void DeleteProduct_is_bad_request()
    {
        //given
        int id = 1;
        moqDbService.Setup(m => m.DeleteProduct(It.IsAny<int>())).Returns(false);

        var sut = new ProductsController(moqDbService.Object);

        //when
        IActionResult actionResult = sut.DeleteProduct(id);
        var contentResult = actionResult as BadRequestResult;

        ////then
        Assert.IsNotNull(contentResult);
        Assert.AreEqual(400, contentResult.StatusCode);
    }
}
}