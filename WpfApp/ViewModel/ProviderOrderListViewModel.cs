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

namespace WpfApp.ViewModel
{
    internal class ProviderOrderListViewModel : ViewModelBase
    {
        private ObservableCollection<ProviderOrder> _providerordersList;


        private ObservableCollection<Article> _articlesList;
        private ObservableCollection<Address> _addressList;
        private ObservableCollection<Status> _statusList;
        private ObservableCollection<Provider> _providerList;

        private bool _visibilityMenu;
        private ProviderOrder _selectProviderOrder;
        private Address _selectAddress;

        #region "Property"

        public ObservableCollection<ProviderOrder> ProviderOrdersList
        {
            get { return _providerordersList; }
            set { SetProperty(ref _providerordersList, value); }
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

        public ProviderOrder SelectProviderOrder
        {
            get { return _selectProviderOrder; }
            set { SetProperty(ref _selectProviderOrder, value); }
        }

        public Address SelectAddress
        {
            get { return _selectAddress; }
            set { SetProperty(ref _selectAddress, value); }
        }

        public ObservableCollection<Provider> ProviderList
        {
            get { return _providerList; }
            set { SetProperty(ref _providerList, value); }
        }

        public ObservableCollection<Article> ArticlesList
        {
            get { return _articlesList; }
            set { SetProperty(ref _articlesList, value); }
        }

        #endregion

        public ProviderOrderListViewModel()
        {
            VisibleModalDroiteCommand = new ViewModelCommand<ProviderOrder>(ExecuteVisibleModalDroiteCommand);
            UnvisibleModalDroiteCommand = new ViewModelCommand<object>(ExecuteUnvisibleModalDroiteCommand);

            SaveOrderCommand = new ViewModelCommand<object>(ExecuteSaveOrderCommand);
            CreateOrderCommand = new ViewModelCommand<object>(ExecuteCreateOrderCommand);
            SaveNewOrderCommand = new ViewModelCommand<object>(ExecuteSaveNewOrderCommand);
            AddOrderCommand = new ViewModelCommand<object>(ExecuteAddOrderCommand);
            DeleteOrderCommand = new ViewModelCommand<ProviderOrder>(ExecuteDeleteOrderCommand);

            RefreshOrder = new ViewModelCommand<object>(ExecuteRefreshOrderCommand);

            GetProviderOrders();
            GetStatus();
            GetCategories();
            GetProviders();
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

        private async void GetProviderOrders()
        {
            ProviderOrdersList = new ObservableCollection<ProviderOrder>();
            var content = await ModeCommun.client.GetStringAsync("ProviderOrder");
            ProviderOrdersList =
                new ObservableCollection<ProviderOrder>(JsonConvert.DeserializeObject<List<ProviderOrder>>(content));
        }

        private async void GetStatus()
        {
            var content = await ModeCommun.client.GetStringAsync("Status");
            StatusList = new ObservableCollection<Status>(JsonConvert.DeserializeObject<List<Status>>(content));
        }

        private async void GetCategories()
        {
            //var content = await ModeCommun.client.GetStringAsync("Address");
            //AddressList = new ObservableCollection<Address>(JsonConvert.DeserializeObject<List<Address>>(content));
        }

        private async void GetProviders()
        {
            var content = await ModeCommun.client.GetStringAsync("Provider");
            ProviderList = new ObservableCollection<Provider>(JsonConvert.DeserializeObject<List<Provider>>(content));
        }

        #endregion


        #region "Command"

        public ICommand VisibleModalDroiteCommand { get; }

        private void ExecuteVisibleModalDroiteCommand(ProviderOrder obj)
        {
            SelectProviderOrder = obj;
            VisibilityMenu = true;
            //foreach (var article in ArticlesList)
            //{
            //    if (SelectProviderOrder.ArticleOrders.Select(o => o.Article.Id).Contains(article.Id))
            //    {
            //        article.IsSelected = true;
            //        article.NbArticleCommand = SelectProviderOrder.ArticleOrders.FirstOrDefault<ArticleOrder>(u => u.Article.Id == article.Id).Quantity;
            //    }
            //    else
            //    {
            //        article.IsSelected = false;
            //        article.NbArticleCommand = 0;
            //    }
            //}
        }


        public ICommand DeleteOrderCommand { get; }

        private async void ExecuteDeleteOrderCommand(ProviderOrder obj)
        {
            SelectProviderOrder = obj;
            if (MessageBox.Show("Are you sure you want to delete this order?", "Warning", MessageBoxButton.YesNo,
                    MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                var response = await ModeCommun.client.DeleteAsync("providerorder/" + SelectProviderOrder.Id);
                ProviderOrdersList.Remove(SelectProviderOrder);
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
            if (SelectProviderOrder.Id == 0)
            {
                SelectProviderOrder.ProviderId = SelectProviderOrder.Provider.Id;
                SelectProviderOrder.Provider = null;

                var response = await ModeCommun.client.PostAsJsonAsync("providerorder", SelectProviderOrder);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    SelectProviderOrder = JsonConvert.DeserializeObject<ProviderOrder>(content);

                    foreach (var article in ArticlesList)
                    {
                        if (article.IsSelected)
                        {
                            var ArticleOrder = new ArticleOrder();
                            ArticleOrder.ArticleId = article.Id;
                            ArticleOrder.ProviderOrderId = SelectProviderOrder.Id;
                            ArticleOrder.Quantity = article.NbArticleCommand;

                            response = await ModeCommun.client.PostAsJsonAsync("articleorder", ArticleOrder);
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                content = await response.Content.ReadAsStringAsync();
                                ArticleOrder = JsonConvert.DeserializeObject<ArticleOrder>(content);

                                GetProviderOrders();
                                VisibilityMenu = false;
                            }
                        }
                    }
                }
            }
            else
            {
                var articleOrders = new List<ArticleOrder>();
                articleOrders = SelectProviderOrder.ArticleOrders;

                SelectProviderOrder.Provider = null;
                SelectProviderOrder.ArticleOrders = null;
                SelectProviderOrder.Status = null;

                var response =
                    await ModeCommun.client.PutAsJsonAsync("providerorder/" + SelectProviderOrder.Id, SelectProviderOrder);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    SelectProviderOrder = JsonConvert.DeserializeObject<ProviderOrder>(content);

                    foreach (var article in ArticlesList)
                    {

                        if (SelectProviderOrder.StatusId == 3 && article.NbArticleCommand != 0)
                        {
                            var numberToRestock = article.NbArticleCommand;
                            
                            var responseArticle = await ModeCommun.client.GetAsync("article/" + article.Id);
                            if (responseArticle.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                var contentArticle = await responseArticle.Content.ReadAsStringAsync();
                                var articleToRestock = JsonConvert.DeserializeObject<Article>(contentArticle);
                                articleToRestock.Stock += numberToRestock;
                                responseArticle = await ModeCommun.client.PutAsJsonAsync("article/" + article.Id, articleToRestock);
                            }
                        }
                        
                        if (article.IsSelected)
                        {
                            var ArticleOrder = new ArticleOrder();
                            ArticleOrder.ArticleId = article.Id;
                            ArticleOrder.ProviderOrderId = SelectProviderOrder.Id;
                            ArticleOrder.Quantity = article.NbArticleCommand;

                            var deleteArticleOrder =
                                articleOrders.FirstOrDefault<ArticleOrder>(u => u.Article.Id == article.Id);
                            if (deleteArticleOrder != null)
                            {
                                response = await ModeCommun.client.DeleteAsync("articleorder/" +
                                                                               deleteArticleOrder.Id);
                                articleOrders.Remove(deleteArticleOrder);
                            }

                            response = await ModeCommun.client.PostAsJsonAsync("articleorder", ArticleOrder);
                            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                content = await response.Content.ReadAsStringAsync();
                                ArticleOrder = JsonConvert.DeserializeObject<ArticleOrder>(content);

                                GetProviderOrders();
                                VisibilityMenu = false;
                            }
                        }
                    }
                }
            }
        }


        public ICommand CreateOrderCommand { get; }

        private async void ExecuteCreateOrderCommand(object obj)
        {
            SelectProviderOrder = new ProviderOrder();
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
            SelectProviderOrder = new ProviderOrder();
            VisibilityMenu = true;
        }


        public ICommand AddOrderCommand { get; }

        private async void ExecuteAddOrderCommand(object obj)
        {
            SelectProviderOrder = new ProviderOrder();
            SelectAddress = new Address();
            VisibilityMenu = true;
        }

        public ICommand RefreshOrder { get; }

        public async void ExecuteRefreshOrderCommand(object obj)
        {
            GetProviderOrders();
            GetStatus();
            GetCategories();
            GetProviders();
        }

        #endregion
    }
}