using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Entities;
using Bogus;
namespace WebApi.Test.SampleData
{
    internal class TestData
    {
        public static IEnumerable<Product> GetData(int count = 5, string brand = "")
        {
            var products=new Faker<Product>()
            .RuleFor(id=>id.Id,f=>f.Random.Int())
            .RuleFor(d=>d.Description,f => string.Join(", ", f.Lorem.Words()))
            .RuleFor(b=>b.Brand,f=>string.IsNullOrEmpty(brand)? f.Lorem.Word():brand)
            .RuleFor(m=>m.Model,f=>f.Lorem.Word());
            return products.Generate(count);
        }
    }
}