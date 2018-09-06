using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Entities;
using WebApi.Helpers;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebApi.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace WebApi.Services
{
    public interface IProductService
    {
        List<Product> GetProducts(string q);

        Product GetProduct(int id);

        Product AddProduct(Product product);

        bool UpdateProduct(Product product);

        bool DeleteProduct(int id);
    }

    internal class ProductService : IProductService
    {
        private DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public virtual List<Product> GetProducts(string q = "")
        {
            try
            {
                var allProduct = _context.Products.AsQueryable();
                q = q.Trim().ToLowerInvariant();
                if (!string.IsNullOrEmpty(q))
                {
                    allProduct = allProduct.Where(m => m.Description.ToLowerInvariant().Contains(q)
                        || m.Model.ToLowerInvariant().Contains(q) || m.Brand.ToLowerInvariant().Contains(q));
                }

                return allProduct.ToList();
            }
            catch (AppException)
            {
               return null; //shout/catch/throw/log
            }             
        }

        public virtual Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public virtual Product AddProduct(Product product)
        {
            try
            {
                _context.Add(product);
                _context.SaveChanges();
                return product;
            }
            catch (AppException)
            {
               return null; //shout/catch/throw/log
            }             
        }

        public virtual bool DeleteProduct(int id)
        {
            int row = 0;
            try
            {
                var product = _context.Products.Find(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    row = _context.SaveChanges();
                }
            }
            catch (AppException)
            {
               return false; //shout/catch/throw/log
            }
            return row == 0 ? false : true;
        }

        public virtual bool UpdateProduct(Product product)
        {
            try
            {
                var update = _context.Products.Update(product);
                _context.SaveChanges();
                return update is null ? false : true;
            }
            catch (AppException)
            {
               return false; //shout/catch/throw/log
            }                         

        }
    }
}