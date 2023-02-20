using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.InteropServices.JavaScript;
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

        private bool _visibilityCreateMenu;
        private bool _visibilityEditMenu;

        private Article _selectArticle;

        #region "Property"

        public ObservableCollection<Article> ArticlesList
        {
            get { return _articlesList; }
            set { SetProperty(ref _articlesList, value); }
        }

        public bool VisibilityCreateMenu
        {
            get { return _visibilityCreateMenu; }
            set { SetProperty(ref _visibilityCreateMenu, value); }
        }

        public bool VisibilityEditMenu
        {
            get { return _visibilityEditMenu; }
            set { SetProperty(ref _visibilityEditMenu, value); }
        }

        public Article SelectArticle
        {
            get { return _selectArticle; }
            set { SetProperty(ref _selectArticle, value); }
        }

        #endregion

        public ICommand ToggleAddMenu { get; }
        public ICommand CreateArticleCommand { get; }

        public ICommand ToggleEditMenu { get; }
        public ICommand SaveArticleCommand { get; }

        public ICommand DeleteArticleCommand { get; }


        public ArticleListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Article>(ExecuteToggleAddMenu);
            CreateArticleCommand = new ViewModelCommand<Article>(ExecuteCreateArticleCommand);

            ToggleEditMenu = new ViewModelCommand<Article>(ExecuteToggleEditMenu);
            SaveArticleCommand = new ViewModelCommand<Article>(ExecuteSaveArticleCommand);

            DeleteArticleCommand = new ViewModelCommand<Article>(ExecuteDeleteArticleCommand);

            GetArticles();
        }


        private async void GetArticles()
        {
            var content = await ModeCommun.client.GetStringAsync("Article");
            ArticlesList = new ObservableCollection<Article>(JsonConvert.DeserializeObject<List<Article>>(content));
        }


        private void ExecuteToggleAddMenu(Article obj)
        {
            SelectArticle = new Article();
            VisibilityCreateMenu = !VisibilityCreateMenu;
        }

        private async void ExecuteCreateArticleCommand(Object obj)
        {
            Article article = new Article();


            var response = await ModeCommun.client.PostAsJsonAsync("Article", obj);
            VisibilityCreateMenu = !VisibilityCreateMenu;
            GetArticles();
        }


        private void ExecuteToggleEditMenu(Article obj)
        {
            SelectArticle = obj;
            VisibilityEditMenu = !VisibilityEditMenu;
        }

        private async void ExecuteSaveArticleCommand(Article obj)
        {
            obj.DomainId = obj.Domain.Id;
            obj.CategoryId = obj.Category.Id;
            
            var response = await ModeCommun.client.PutAsJsonAsync("Article/" + SelectArticle.Id, obj);
            VisibilityEditMenu = !VisibilityEditMenu;
            GetArticles();
        }


        private async void ExecuteDeleteArticleCommand(Article obj)
        {
            var response = await ModeCommun.client.DeleteAsync("Article/" + obj.Id);
            GetArticles();
        }
    }
}