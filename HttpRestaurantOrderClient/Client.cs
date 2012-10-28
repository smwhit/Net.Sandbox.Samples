using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpRestaurantOrderClient
{
    public class Client
    {
        public static void Main(string[] args)
        {
            RestaurantOrderService.RestaurantOrderClient client = new RestaurantOrderService.RestaurantOrderClient();
            Console.WriteLine(client.MakeOrder(new string[0]));
            Console.ReadLine();
        }
    }
}
