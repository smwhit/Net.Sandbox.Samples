using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IISClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.RestaurantOrderClient client = new ServiceReference1.RestaurantOrderClient();
            Console.WriteLine(client.MakeOrder(new string[0]));
            Console.ReadLine();
        }
    }
}
