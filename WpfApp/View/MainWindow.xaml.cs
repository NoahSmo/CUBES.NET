using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
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
using Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WpfApp.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImpvaG4uZG9lQGdtYWlsLmNvbSIsIm5hbWVpZCI6IjEiLCJyb2xlIjoiQWRtaW4iLCJQZXJtaXNzaW9uIjoiQ3JlYXRlLFJlYWQsVXBkYXRlLERlbGV0ZSIsIm5iZiI6MTY3NTQzMjc5MSwiZXhwIjoxNjc1NDMzMzkxLCJpYXQiOjE2NzU0MzI3OTEsImlzcyI6Ik5FR09TVUQiLCJhdWQiOiJVU0VSUyBBTkQgQURNSU5TIn0.907vkoTd8fmnzHyn1-TKVs84v1CMBPt4_N_a9Dpj6hA";

        private ProductListViewModel _productListDataContext;

        //public ProductListViewModel ProductListDataContext
        //{
        //    get { return _productListDataContext; }
        //    set { _productListDataContext = value; }
        //}

        private string _productName = "Louis le bg";

        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                _productName= value;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            //ModeCommun.client = new HttpClient();
            //ModeCommun.client.BaseAddress = new Uri("https://localhost:44301/api/");
            //ModeCommun.client.DefaultRequestHeaders.Accept.Clear();
            //ModeCommun.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //ModeCommun.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);

            GetUsers();

        }

        private async void GetUsers()
        {
            //var content = await client.GetStringAsync("User");
            //var users = JsonConvert.DeserializeObject<List<User>>(content);
        }



    }
}
