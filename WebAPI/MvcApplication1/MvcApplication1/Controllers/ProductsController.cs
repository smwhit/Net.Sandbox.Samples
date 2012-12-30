using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcApplication1.Controllers
{
    public class ProductsController : ApiController
    {
        Product[] products = new Product[] {
            new Product { Id = 1, Category = "Groceries", Name= "Tomato Soup", Price = 1.0m},
            new Product { Id = 2, Category = "Toys", Name= "Yo Yo", Price = 1.0m},
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        public Product GetProductById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return products.Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }
    }
}
