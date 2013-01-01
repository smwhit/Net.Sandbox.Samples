using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.ProductStoreClient
{
    class ProductsCollection : ObservableCollection<Product>
    {
        public void CopyFrom(IEnumerable<Product> products)
        {
            Items.Clear();
            foreach (var p in products)
            {
                this.Items.Add(p);
            }

            this.OnCollectionChanged(new System.Collections.Specialized.NotifyCollectionChangedEventArgs(System.Collections.Specialized.NotifyCollectionChangedAction.Reset));
        }
    }
}
