using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ProductStoreClient
{
    class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:50678/");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/products").Result;//blocking
            
            if(response.IsSuccessStatusCode) {
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                foreach (var p in products)
	            {
		            Console.WriteLine("{0}\t{1};\t{2}", p.Name, p.Price, p.Category);
	            }
            }
            else {
                Console.WriteLine("{0} ({1})", response.StatusCode, response.ReasonPhrase);
            }
        }
    }
}
