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
    internal class ArticleListViewModel : ViewModelBase
    {
        private string _articleName;
        private string _articleDescription;
        private float _articleYear;
        private float _articlePrice;
        private float _articleAlcohol;

        #region "Property"

        public string ArticleName
        {
            get { return _articleName; }
            set { SetProperty(ref _articleName, value); }
        }

        public string ArticleDescription
        {
            get { return _articleDescription; }
            set { SetProperty(ref _articleDescription, value); }
        }

        public float ArticleYear
        {
            get { return _articleYear; }
            set { SetProperty(ref _articleYear, value); }
        }

        public float ArticlePrice
        {
            get { return _articlePrice; }
            set { SetProperty(ref _articlePrice, value); }
        }

        public float ArticleAlcohol
        {
            get { return _articleAlcohol; }
            set { SetProperty(ref _articleAlcohol, value); }
        }

        #endregion

        public ICommand AddArticleCommand { get; set; }
        public ICommand UpdateArticleCommand { get; set; }
        public ICommand DeleteArticleCommand { get; set; }

        public ArticleListViewModel()
        {
            GetArticles();
        }
        
        public ArticleListViewModel(Article article)
        {
            ArticleName = article.Name;
            ArticleDescription = article.Description;
            ArticleYear = article.Year;
            ArticlePrice = article.Price;
            ArticleAlcohol = article.Alcohol;
        }

        private async void GetArticles()
        {
            var content = await ModeCommun.client.GetStringAsync("Article");
            var ListArticle = JsonConvert.DeserializeObject<List<Article>>(content);
            
            foreach (var article in ListArticle)
            {
                ArticleListViewModel articleListViewModel = new ArticleListViewModel(article);
            }
        }
    }
}