using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using ArticleOrder = Api.Models.ArticleOrder;

namespace WpfApp.ViewModel
{
    internal class OrderListViewModel : ViewModelBase
    {
        private ObservableCollection<Order> _ordersList;
        private ObservableCollection<Article> _articlesList;
        private ObservableCollection<Address> _addressList;
        private ObservableCollection<Status> _statusList;
        private ObservableCollection<User> _usersList;

        private bool _visibilityMenu;
        private Order _selectOrder;
        private Address _selectAddress;

        #region "Property"

        public ObservableCollection<Order> OrdersList
        {
            get { return _ordersList; }
            set { SetProperty(ref _ordersList, value); }
        }

        public ObservableCollection<Status> StatusList
        {
            get { return _statusList; }
            set { SetProperty(ref _statusList, value); }
        }

        public ObservableCollection<Address> AddressList
        {
            get { return _addressList; }
            set { SetProperty(ref _addressList, value); }
        }

        public bool VisibilityMenu
        {
            get { return _visibilityMenu; }
            set { SetProperty(ref _visibilityMenu, value); }
        }

        public Order SelectOrder
        {
            get { return _selectOrder; }
            set { SetProperty(ref _selectOrder, value); }
        }

        public Address SelectAddress
        {
            get { return _selectAddress; }
            set { SetProperty(ref _selectAddress, value); }
        }

        public ObservableCollection<User> UsersList
        {
            get { return _usersList; }
            set { SetProperty(ref _usersList, value); }
        }

        public ObservableCollection<Article> ArticlesList
        {
            get { return _articlesList; }
            set { SetProperty(ref _articlesList, value); }
        }

        #endregion

        public OrderListViewModel()
        {
            VisibleModalDroiteCommand = new ViewModelCommand<Order>(ExecuteVisibleModalDroiteCommand);
            UnvisibleModalDroiteCommand = new ViewModelCommand<object>(ExecuteUnvisibleModalDroiteCommand);

            SaveOrderCommand = new ViewModelCommand<object>(ExecuteSaveOrderCommand);
            CreateOrderCommand = new ViewModelCommand<object>(ExecuteCreateOrderCommand);
            SaveNewOrderCommand = new ViewModelCommand<object>(ExecuteSaveNewOrderCommand);
            AddOrderCommand = new ViewModelCommand<object>(ExecuteAddOrderCommand);
            DeleteOrderCommand = new ViewModelCommand<Order>(ExecuteDeleteOrderCommand);

            RefreshOrder = new ViewModelCommand<object>(ExecuteRefreshOrderCommand);

            GetOrders();
            GetStatus();
            GetUsers();
            GetArticles();
        }


        #region "Get"

        private async void GetArticles()
        {
            ArticlesList = new ObservableCollection<Article>();
            var content = await ModeCommun.client.GetStringAsync("Article");
            ArticlesList = new ObservableCollection<Article>(JsonConvert.DeserializeObject<List<Article>>(content));

            foreach (var article in ArticlesList)
            {
                article.Category = null;
                article.Provider = null;
            }
        }

        private async void GetOrders()
        {
            OrdersList = new ObservableCollection<Order>();
            var content = await ModeCommun.client.GetStringAsync("Order");
            OrdersList = new ObservableCollection<Order>(JsonConvert.DeserializeObject<List<Order>>(content));
        }

        private async void GetStatus()
        {
            var content = await ModeCommun.client.GetStringAsync("Status");
            StatusList = new ObservableCollection<Status>(JsonConvert.DeserializeObject<List<Status>>(content));
        }

        private async void GetUsers()
        {
            var content = await ModeCommun.client.GetStringAsync("User/wpf");
            UsersList = new ObservableCollection<User>(JsonConvert.DeserializeObject<List<User>>(content));
        }

        #endregion


        #region "Command"

        public ICommand VisibleModalDroiteCommand { get; }

        private void ExecuteVisibleModalDroiteCommand(Order obj)
        {
            SelectOrder = obj;
            SelectAddress = obj.Address;
            VisibilityMenu = true;
            foreach (var article in ArticlesList)
            {
                if (SelectOrder.ArticleOrders.Select(o => o.Article.Id).Contains(article.Id))
                {
                    article.IsSelected = true;
                    article.NbArticleCommand = SelectOrder.ArticleOrders
                        .FirstOrDefault<ArticleOrder>(u => u.Article.Id == article.Id).Quantity;
                }
                else
                {
                    article.IsSelected = false;
                    article.NbArticleCommand = 0;
                }
            }
        }


        public ICommand DeleteOrderCommand { get; }

        private async void ExecuteDeleteOrderCommand(Order obj)
        {
            SelectOrder = obj;
            if (MessageBox.Show("Are you sure you want to delete this order?", "Warning", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                var response = await ModeCommun.client.DeleteAsync("order/" + SelectOrder.Id);
                OrdersList.Remove(SelectOrder);
            }
        }


        public ICommand UnvisibleModalDroiteCommand { get; }

        private void ExecuteUnvisibleModalDroiteCommand(object obj)
        {
            VisibilityMenu = false;
        }


        public ICommand SaveOrderCommand { get; }

        private async void ExecuteSaveOrderCommand(object obj)
        {
            if (SelectOrder.Id == 0)
            {
                var response = await ModeCommun.client.PostAsJsonAsync("address", SelectAddress);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    SelectAddress = JsonConvert.DeserializeObject<Address>(content);

                    SelectOrder.AddressId = SelectAddress.Id;
                    SelectOrder.UserId = SelectOrder.User.Id;
                    SelectOrder.User = null;

                    response = await ModeCommun.client.PostAsJsonAsync("order", SelectOrder);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        SelectOrder = JsonConvert.DeserializeObject<Order>(content);

                        foreach (var article in ArticlesList)
                        {
                            if (article.IsSelected)
                            {
                                var ArticleOrder = new ArticleOrder();
                                ArticleOrder.ArticleId = article.Id;
                                ArticleOrder.OrderId = SelectOrder.Id;
                                ArticleOrder.Quantity = article.NbArticleCommand;

                                response = await ModeCommun.client.PostAsJsonAsync("articleorder", ArticleOrder);
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    content = await response.Content.ReadAsStringAsync();
                                    ArticleOrder = JsonConvert.DeserializeObject<ArticleOrder>(content);

                                    GetOrders();
                                    VisibilityMenu = false;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var response = await ModeCommun.client.PutAsJsonAsync("address/" + SelectAddress.Id, SelectAddress);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    SelectAddress = JsonConvert.DeserializeObject<Address>(content);

                    var articleOrders = new List<ArticleOrder>();
                    articleOrders = SelectOrder.ArticleOrders;
                    
                    SelectOrder.AddressId = SelectAddress.Id;
                    SelectOrder.UserId = SelectOrder.User.Id;
                    SelectOrder.User = null;
                    SelectOrder.ArticleOrders = null;
                    SelectOrder.Status = null;
                    SelectOrder.Address = null;

                    response = await ModeCommun.client.PutAsJsonAsync("order/" + SelectOrder.Id, SelectOrder);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        content = await response.Content.ReadAsStringAsync();
                        SelectOrder = JsonConvert.DeserializeObject<Order>(content);

                        foreach (var article in ArticlesList)
                        {
                            if (article.IsSelected)
                            {
                                var ArticleOrder = new ArticleOrder();
                                ArticleOrder.ArticleId = article.Id;
                                ArticleOrder.OrderId = SelectOrder.Id;
                                ArticleOrder.Quantity = article.NbArticleCommand;
                                
                                var deleteArticleOrder = articleOrders.FirstOrDefault<ArticleOrder>(u => u.Article.Id == article.Id);
                                if (deleteArticleOrder != null)
                                {
                                    response = await ModeCommun.client.DeleteAsync("articleorder/" + deleteArticleOrder.Id);
                                    articleOrders.Remove(deleteArticleOrder);
                                }

                                response = await ModeCommun.client.PostAsJsonAsync("articleorder", ArticleOrder);
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    content = await response.Content.ReadAsStringAsync();
                                    ArticleOrder = JsonConvert.DeserializeObject<ArticleOrder>(content);

                                    GetOrders();
                                    VisibilityMenu = false;
                                }
                            }
                        }
                    }
                }
            }
        }


        public ICommand CreateOrderCommand { get; }

        private async void ExecuteCreateOrderCommand(object obj)
        {
            SelectOrder = new Order();
            VisibilityMenu = true;
            foreach (var article in ArticlesList)
            {
                article.IsSelected = false;
                article.NbArticleCommand = 0;
            }
        }


        public ICommand SaveNewOrderCommand { get; }

        private async void ExecuteSaveNewOrderCommand(object obj)
        {
            SelectOrder = new Order();
            SelectOrder.Address = new Address();
            VisibilityMenu = true;
        }


        public ICommand AddOrderCommand { get; }

        private async void ExecuteAddOrderCommand(object obj)
        {
            SelectOrder = new Order();
            SelectAddress = new Address();
            VisibilityMenu = true;
            
            foreach (var article in ArticlesList)
            {
                article.IsSelected = false;
                article.NbArticleCommand = 0;
            }
        }

        
        
        
        
        public ICommand RefreshOrder { get; }
        public async void ExecuteRefreshOrderCommand(object obj)
        {
            GetOrders();
            GetStatus();
            GetUsers();
        }

        #endregion
    }
}