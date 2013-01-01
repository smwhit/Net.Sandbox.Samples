using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF.ProductStoreClient
{   
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        ProductsCollection products = new ProductsCollection();

        public MainWindow()
        {
            InitializeComponent();

            client.BaseAddress = new Uri("http://localhost:50678");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            this.ProductsList.ItemsSource = products;
        }

        private async void btnGetProducts_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                btnGetProducts.IsEnabled = false;
                var response = await client.GetAsync("api/products");
                response.EnsureSuccessStatusCode();

                var products = await response.Content.ReadAsAsync<IEnumerable<Product>>();
                this.products.CopyFrom(products);
            }
            catch (Newtonsoft.Json.JsonException jex)
            {
                MessageBox.Show(jex.Message);
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnGetProducts.IsEnabled = true;
            }
        }
    }
}
