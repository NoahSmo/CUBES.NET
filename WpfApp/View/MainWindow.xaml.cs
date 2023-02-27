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
using WpfApp.View;
using WpfApp.ViewModel;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void RedirectArticleList(object sender, RoutedEventArgs e)
        {
            CurrentView.Navigate(new ArticleListView());
            
            DeleteLastView(sender, e);
        }
        
        private void RedirectCategoryList(object sender, RoutedEventArgs e)
        {
            CurrentView.Navigate(new CategoryListView());
            
            DeleteLastView(sender, e);
        }
        
        private void RedirectUserList(object sender, RoutedEventArgs e)
        {
            CurrentView.Navigate(new UserListView());
            
            DeleteLastView(sender, e);
        }
        
        private void DeleteLastView(object sender, RoutedEventArgs e)
        {
            while (CurrentView.CanGoBack)
            {
                CurrentView.RemoveBackEntry();
            }
        }
    }
}
