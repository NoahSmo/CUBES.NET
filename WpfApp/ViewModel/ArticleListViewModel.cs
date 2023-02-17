using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Api.Models;
using Newtonsoft.Json;

namespace WpfApp.ViewModel
{
    internal class ArticleListViewModel : ViewModelBase
    {
        private ObservableCollection<Article> _articlesList;

        private bool _visibilityModalDroite;
        private Article _selectArticle;

        #region "Property"

        public ObservableCollection<Article> ArticlesList
        {
            get { return _articlesList; }
            set {SetProperty(ref _articlesList , value); }
        }

        public bool VisibilityModalDroite
        {
            get { return _visibilityModalDroite; }
            set {SetProperty(ref _visibilityModalDroite , value); }
        }

        public Article SelectArticle
        {
            get { return _selectArticle; }
            set {SetProperty(ref _selectArticle , value); }
        }

        #endregion

        public ICommand AddArticleCommand { get; set; }
        public ICommand UpdateArticleCommand { get; set; }
        public ICommand DeleteArticleCommand { get; set; }
        public ICommand VisibleModalDroiteCommand { get; }
        public ICommand UnvisibleModalDroiteCommand { get; }
        public ICommand SaveArticleCommand { get; }
        public ICommand CreateArticleCommand { get; }
        public ICommand SaveNewArticleCommand { get; }

        public ArticleListViewModel()
        {
            VisibleModalDroiteCommand = new ViewModelCommand<Article>(ExecuteVisibleModalDroiteCommand);
            UnvisibleModalDroiteCommand = new ViewModelCommand<object>(ExecuteUnvisibleModalDroiteCommand);
            SaveArticleCommand = new ViewModelCommand<object>(ExecuteSaveArticleCommand);
            CreateArticleCommand = new ViewModelCommand<object>(ExecuteCreateArticleCommand);
            SaveNewArticleCommand = new ViewModelCommand<object>(ExecuteSaveNewArticleCommand);
            GetArticles();
        }
        

        private async void GetArticles()
        {
            var content = await ModeCommun.client.GetStringAsync("Article");
            ArticlesList = new ObservableCollection<Article>( JsonConvert.DeserializeObject<List<Article>>(content));
            //foreach (var article in ListArticle)
            //{
            //    ArticleListViewModel articleListViewModel = new ArticleListViewModel(article);
            //}
        }


        private void ExecuteVisibleModalDroiteCommand(Article obj)
        {
            SelectArticle = obj;
            VisibilityModalDroite = true;
        }
        
        private void ExecuteUnvisibleModalDroiteCommand(object obj)
        {
            VisibilityModalDroite = false;
        } 
        
        private async void ExecuteSaveArticleCommand(object obj)
        {
            var response = await ModeCommun.client.PutAsJsonAsync("article/1" , SelectArticle);
        }
        
        private async void ExecuteCreateArticleCommand(object obj)
        {
            SelectArticle = new Article();
            VisibilityModalDroite = true;
        }
        
        private async void ExecuteSaveNewArticleCommand(object obj)
        {
            SelectArticle = new Article();
            VisibilityModalDroite = true;
        }
    }
}