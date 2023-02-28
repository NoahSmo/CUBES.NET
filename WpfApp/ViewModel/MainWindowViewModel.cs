using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp;
using Newtonsoft.Json;

namespace WpfApp.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        
        private ArticleListViewModel _articleListDataContext;
        public ArticleListViewModel ArticleListDataContext
        {
            get { return _articleListDataContext; }
            set { SetProperty(ref _articleListDataContext , value); }
        }
        
        private ProviderListViewModel _providerListDataContext;
        public ProviderListViewModel ProviderListDataContext
        {
            get { return _providerListDataContext; }
            set { SetProperty(ref _providerListDataContext , value); }
        }
        private CategoryListViewModel _categoryListDataContext;
        public CategoryListViewModel CategoryListDataContext
        {
            get { return _categoryListDataContext; }
            set { SetProperty(ref _categoryListDataContext , value); }
        }
        private UserListViewModel _userListDataContext;
        public UserListViewModel UserListDataContext
        {
            get { return _userListDataContext; }
            set { SetProperty(ref _userListDataContext, value); }
        }

        private GestionStockViewModel _gestionStockDataContext;
        public GestionStockViewModel GestionStockDataContext
        {
            get { return _gestionStockDataContext; }
            set { SetProperty(ref _gestionStockDataContext, value); }
        }
        
        private ImageListViewModel _imageDataContext;
        public ImageListViewModel ImageDataContext
        {
            get { return _imageDataContext; }
            set { SetProperty(ref _imageDataContext, value); }
        }

        private OrderListViewModel _orderListDataContext;
        public OrderListViewModel OrderListDataContext
        {
            get { return _orderListDataContext; }
            set { SetProperty (ref _orderListDataContext , value); }
        }

        private ProviderOrderListViewModel _providerOrderListDataContext;
        public ProviderOrderListViewModel ProviderOrderListDataContext
        {
            get { return _providerOrderListDataContext; }
            set { SetProperty (ref _providerOrderListDataContext , value); }
        }

        private int _indexTabItem;
        public int IndexTabItem
        {
            get { return _indexTabItem; }
            set { 
                SetProperty (ref _indexTabItem , value);
                switch( _indexTabItem)
                {
                    case 0:
                        ArticleListDataContext.ExecuteRefreshArticleCommand(null);
                        break;
                    case 1:
                        CategoryListDataContext.ExecuteRefreshCategoryCommand(null);
                        break;
                    case 2:
                        ProviderListDataContext.ExecuteRefreshProviderCommand(null);
                        break;
                    case 3:
                        UserListDataContext.ExecuteRefreshUserCommand(null);
                        break;
                    case 4:
                        OrderListDataContext.ExecuteRefreshOrderCommand(null);
                        break;
                    case 5:
                        ProviderOrderListDataContext.ExecuteRefreshOrderCommand(null);
                        break;
                    case 6:
                        GestionStockDataContext.GetArticles();
                        break;
                    case 7:
                        ImageDataContext.ExecuteRefreshImageCommand(null);
                        break;
                }
            }
        }
        public MainWindowViewModel()
        {  
            ArticleListDataContext = new ArticleListViewModel();
            CategoryListDataContext = new CategoryListViewModel();
            UserListDataContext = new UserListViewModel();
            GestionStockDataContext = new GestionStockViewModel();
            ProviderListDataContext = new ProviderListViewModel();
            OrderListDataContext = new OrderListViewModel();
            ImageDataContext = new ImageListViewModel();
            ProviderOrderListDataContext = new ProviderOrderListViewModel();
        }
    }
}
