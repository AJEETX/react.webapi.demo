using WebApi.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using WebApi.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("products")]
    public class ProductsController : Controller
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{q?}")]
        [ProducesResponseType(200, Type = typeof(List<Product>))]
        [ProducesResponseType(400)]
        public IActionResult GetProducts(string q = "")
        {
            try
            {
                var products = _productService.GetProducts(q);
                return Ok(products);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);//shout/catch/throw/log
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _productService.GetProduct(id);
                if (product == null) return NotFound();
                return Ok(product);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);//shout/catch/throw/log
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Product))]
        [ProducesResponseType(400)]
        public IActionResult PostProduct([FromBody][Required]Product product)
        {
            if (!ModelState.IsValid || product == null || string.IsNullOrEmpty(product.Description) ||
                string.IsNullOrEmpty(product.Brand) || string.IsNullOrEmpty(product.Model))
                return BadRequest(ModelState);
            try
            {
                _productService.AddProduct(product);
                return Ok(product);
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);//shout/catch/throw/log
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult PutProduct(int id, [FromBody]Product product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                product.Id = id;
                if (!_productService.UpdateProduct(product)) return NotFound();
                return Ok("Product updated");
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);//shout/catch/throw/log
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                if (!_productService.DeleteProduct(id)) return BadRequest();
                return Ok("Product deleted");
            }
            catch (AppException ex)
            {
                return BadRequest(ex.Message);//shout/catch/throw/log
            }
        }
    }
}