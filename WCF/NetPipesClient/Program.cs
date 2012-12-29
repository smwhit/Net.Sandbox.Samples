using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetPipesClient.RestaurantOrderClient;

namespace NetPipesClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RestaurantOrderClient.RestaurantOrderClient client = new RestaurantOrderClient.RestaurantOrderClient();
            Console.WriteLine(client.MakeOrder(new string[0]));
            Console.ReadLine();
        }
    }
}
