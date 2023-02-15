using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Api.Models;
using Newtonsoft.Json;

namespace WpfApp.ViewModel
{
    internal class ArticleListViewModel: ViewModelBase
    {
        private string _articleName;
        private float _articlePrice;
        private string _articleDescription;
        private string _categoryName;
        private string[] _articleImage;
        private string _providerName;
        private int _articleStock;
        
        public string ArticleName
        {
            get
            {
                return _articleName;
            }
            set
            {
                SetProperty(ref _articleName, value);
            }
        }
        
        public float ArticlePrice
        {
            get
            {
                return _articlePrice;
            }
            set
            {
                SetProperty(ref _articlePrice, value);
            }
        }

        public string ArticleDescription
        {
            get
            {
                return _articleDescription;
            }
            set
            {
                SetProperty(ref _articleDescription, value);
            }
        }
        
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                SetProperty(ref _categoryName, value);
            }
        }
        
        public string[] ArticleImage
        {
            get
            {
                return _articleImage;
            }
            set
            {
                SetProperty(ref _articleImage, value);
            }
        }
        
        public string ProviderName
        {
            get
            {
                return _providerName;
            }
            set
            {
                SetProperty(ref _providerName, value);
            }
        }
        
        public int ArticleStock
        {
            get
            {
                return _articleStock;
            }
            set
            {
                SetProperty(ref _articleStock, value);
            }
        }
        
        
        public ICommand AddArticleCommand { get; set; }
        public ICommand UpdateArticleCommand { get; set; }
        public ICommand DeleteArticleCommand { get; set; }

        public ArticleListViewModel()
        {
            ArticleName = "Test Louis 123";
            ArticlePrice = 123.45f;
            ArticleDescription = "Test Description";
            CategoryName = "Test Category";
            ArticleImage = new string[2];
            ArticleImage[0] = "Test Image 1";
            ArticleImage[1] = "Test Image 2";
            ProviderName = "Test Provider";
            ArticleStock = 123;
            GetArticles();
        }
        
        private async void GetArticles()
        {
            var content = await ModeCommun.client.GetStringAsync("Article");
            var ListArticle = JsonConvert.DeserializeObject<List<Article>>(content);
        }
    }
}
