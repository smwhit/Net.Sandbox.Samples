using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Service.Contracts
{
    [ServiceContract]
    public interface RestaurantOrder
    {
        [OperationContract]
        int MakeOrder(IEnumerable<string> items);
    }

    public class CurryRestaurantOrder : RestaurantOrder
    {       
        public int MakeOrder(IEnumerable<string> items)
        {
            return 1;
        }
    }
}
