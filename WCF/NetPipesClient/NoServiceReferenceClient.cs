using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Channels;
using Service.Contracts;
using System.ServiceModel;

namespace NetPipesClient
{
    class NoServiceReferenceClient
    {
        public static void Main(string[] args)
        {
            using (ServiceClient<RestaurantOrder> ServiceClient = new ServiceClient<RestaurantOrder>("NetNamedPipeBinding_RestaurantOrder"))
            {
                //this.Label1.Text = ServiceClient.Proxy.GetMessage();
                Console.WriteLine(ServiceClient.Proxy.MakeOrder(new string[0]));
            }

            Console.ReadLine();
        }
    }

    public class ServiceClient<T> : ClientBase<T> where T : class
    {
        private bool _disposed = false;
        public ServiceClient()
            : base(typeof(T).FullName)
        {
        }
        public ServiceClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }
        public T Proxy
        {
            get { return this.Channel; }
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (this.State == CommunicationState.Faulted)
                    {
                        base.Abort();
                    }
                    else
                    {
                        try
                        {
                            base.Close();
                        }
                        catch
                        {
                            base.Abort();
                        }
                    }
                    _disposed = true;
                }
            }
        }
    }

}
