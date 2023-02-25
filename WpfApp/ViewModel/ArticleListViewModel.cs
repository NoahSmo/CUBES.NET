using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Api.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace WpfApp.ViewModel
{
    internal class ArticleListViewModel : ViewModelBase
    {
        private ObservableCollection<Article> _articlesList;
        private ObservableCollection<Api.Models.Domain> _domainsList;
        private ObservableCollection<Category> _categoriesList;
        private ObservableCollection<Provider> _providersList;

        private bool _visibilityMenu;
        private Article _selectArticle;

        #region "Property"

        public ObservableCollection<Article> ArticlesList
        {
            get { return _articlesList; }
            set {SetProperty(ref _articlesList , value); }
        }

        public ObservableCollection<Api.Models.Domain> DomainsList
        {
            get { return _domainsList; }
            set {SetProperty(ref _domainsList , value); }
        }
        
        public ObservableCollection<Provider> ProvidersList
        {
            get { return _providersList; }
            set {SetProperty(ref _providersList , value); }
        }

        public ObservableCollection<Category> CategoriesList
        {
            get { return _categoriesList; }
            set { SetProperty(ref _categoriesList, value); }
        }

        public bool VisibilityMenu
        {
            get { return _visibilityMenu; }
            set {SetProperty(ref _visibilityMenu , value); }
        }

        public Article SelectArticle
        {
            get { return _selectArticle; }
            set {SetProperty(ref _selectArticle , value); }
        }

        #endregion

        public ArticleListViewModel()
        {
            VisibleModalDroiteCommand = new ViewModelCommand<Article>(ExecuteVisibleModalDroiteCommand);
            UnvisibleModalDroiteCommand = new ViewModelCommand<object>(ExecuteUnvisibleModalDroiteCommand);
            
            SaveArticleCommand = new ViewModelCommand<object>(ExecuteSaveArticleCommand, CanExecuteSaveArticleCommand);
            CreateArticleCommand = new ViewModelCommand<object>(ExecuteCreateArticleCommand);
            SaveNewArticleCommand = new ViewModelCommand<object>(ExecuteSaveNewArticleCommand);
            AddArticleCommand = new ViewModelCommand<object>(ExecuteAddArticleCommand);
            DeleteArticleCommand = new ViewModelCommand<Article>(ExecuteDeleteArticleCommand);
            
            RefreshArticle = new ViewModelCommand<object>(ExecuteRefreshArticleCommand);
            
            GetArticles();
            GetDomains();
            GetProviders();
            GetCategories();
        }


        #region "Get"

        private async void GetArticles()
        {
            ArticlesList = new ObservableCollection<Article>();
            var content = await ModeCommun.client.GetStringAsync("Article");
            ArticlesList = new ObservableCollection<Article>( JsonConvert.DeserializeObject<List<Article>>(content));
            
            foreach (var article in ArticlesList)
            {
                article.Domain = null;
                article.Category = null;
                article.Provider = null;
            }
        }
        
        private async void GetDomains()
        {
            var content = await ModeCommun.client.GetStringAsync("Domain");
            DomainsList = new ObservableCollection<Api.Models.Domain>( JsonConvert.DeserializeObject<List<Api.Models.Domain>>(content));
        }
        
        private async void GetProviders()
        {
            var content = await ModeCommun.client.GetStringAsync("Provider");
            ProvidersList = new ObservableCollection<Provider>( JsonConvert.DeserializeObject<List<Provider>>(content));
        }
        
        private async void GetCategories()
        {
            var content = await ModeCommun.client.GetStringAsync("Category");
            CategoriesList = new ObservableCollection<Category>( JsonConvert.DeserializeObject<List<Category>>(content));
        }

        #endregion


        #region "Command"

        public ICommand VisibleModalDroiteCommand { get; }
        private void ExecuteVisibleModalDroiteCommand(Article obj)
        {
            SelectArticle = obj;
            VisibilityMenu = true;
        }


        public ICommand DeleteArticleCommand { get; }
        private async void ExecuteDeleteArticleCommand(Article obj)
        {
            SelectArticle = obj;
            if(MessageBox.Show("Are you sure you want to delete this article?","Warning", MessageBoxButton.YesNo,MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                var response = await ModeCommun.client.DeleteAsync("article/" + SelectArticle.Id);
                ArticlesList.Remove(SelectArticle);
            }
        }


        public ICommand UnvisibleModalDroiteCommand { get; }
        private void ExecuteUnvisibleModalDroiteCommand(object obj)
        {
            VisibilityMenu = false;
        }


        public ICommand SaveArticleCommand { get; }
        private async void ExecuteSaveArticleCommand(object obj)
        {
            if(SelectArticle.Id == 0)
            {
                var response = await ModeCommun.client.PostAsJsonAsync("article", SelectArticle);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetArticles();
                    VisibilityMenu = false;
                }

            }
            else
            {
                var response = await ModeCommun.client.PutAsJsonAsync("article/"+ SelectArticle.Id , SelectArticle);
            }
        }
        private bool CanExecuteSaveArticleCommand(object obj)
        {
            if (SelectArticle != null)
            {
                bool validData;
                if (SelectArticle.Name == null  || SelectArticle.Description == null || SelectArticle.DomainId == 0 || SelectArticle.CategoryId == 0)
                    validData = false;
                else
                    if (SelectArticle.Name.Length > 0 && SelectArticle.Description.Length > 0)
                    {
                        validData = true;
                    }
                    else
                    {
                        validData=false;
                    }
                return validData;
            }
            return false;
        }


        public ICommand CreateArticleCommand { get; }
        private async void ExecuteCreateArticleCommand(object obj)
        {
            SelectArticle = new Article();
            VisibilityMenu = true;
        }


        public ICommand SaveNewArticleCommand { get; }
        private async void ExecuteSaveNewArticleCommand(object obj)
        {
            SelectArticle = new Article();
            VisibilityMenu = true;
        }


        public ICommand AddArticleCommand { get; }
        private async void ExecuteAddArticleCommand(object obj)
        { 
            SelectArticle = new Article();
            VisibilityMenu = true;
        }
        
        public ICommand RefreshArticle { get; }
        private async void ExecuteRefreshArticleCommand(object obj)
        { 
            GetArticles();
        }

        #endregion

        
    }
}