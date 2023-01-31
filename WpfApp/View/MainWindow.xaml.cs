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
using Api.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        private User CurrentUser = new User();
        private string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImpvaG4uZG9lQGdtYWlsLmNvbSIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTY3NTA2NjY3MiwiZXhwIjoxNjc1MDY3MjcyLCJpYXQiOjE2NzUwNjY2NzIsImlzcyI6Ik5FR09TVUQiLCJhdWQiOiJVU0VSUyBBTkQgQURNSU5TIn0.OoAkdVxi4QlaUSLY2uyDmID4E29ZeXxQ_ELABlfI9jE";


        public MainWindow()
        {
            client.BaseAddress = new Uri("https://localhost:44301/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new  MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);
            InitializeComponent();

            GetUsers();

        }

        private async void GetUsers()
        {
            //var content = await client.GetStringAsync("User");
            //var users = JsonConvert.DeserializeObject<List<User>>(content);
        }
    }
}
