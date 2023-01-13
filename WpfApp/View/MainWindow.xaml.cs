using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
using Newtonsoft.Json;
using WpfApp.Models;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        private User CurrentUser = new User();
        
        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:44301/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new  MediaTypeWithQualityHeaderValue("application/json"));            
            InitializeComponent();

            GetUsers();

        }

        private async void GetUsers()
        {
            var content = await client.GetStringAsync("User");
            var users = JsonConvert.DeserializeObject<List<User>>(content);
            var test = 2;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var test = 2;
        }
    }
}
