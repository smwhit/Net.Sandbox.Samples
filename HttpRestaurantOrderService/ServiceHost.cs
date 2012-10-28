using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Service.Contracts;
using System.ServiceModel;

namespace HttpRestaurantOrderService
{
    public class HttpServiceHost
    {
        public static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(CurryRestaurantOrder));
            serviceHost.Open();

            Console.WriteLine("Press <Enter> to terminate\n\n");
            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
