using FizzWare.NBuilder;
using System;
using System.Collections.Generic;
using System.Text;
using WebApi.Entities;

namespace WebApi.Test.SampleData
{
    internal class TestData
    {
        public static IEnumerable<Product> GetData(int count = 5, string brand = "")
        {
            var products = Builder<Product>.CreateListOfSize(count)
            .All()
                .With(c => c.Id = Faker.RandomNumber.Next())
                .With(c => c.Description = Faker.Company.Name())
                .With(c => c.Brand = string.IsNullOrEmpty(brand) ? Faker.Company.Name() : brand)
                .With(c => c.Model = Faker.Company.Name())
            .Build();
            return products;
        }
    }
}