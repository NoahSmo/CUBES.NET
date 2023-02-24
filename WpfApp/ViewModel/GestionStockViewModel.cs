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

namespace WpfApp.ViewModel
{
    internal class GestionStockViewModel : ViewModelBase
    {

        private ObservableCollection<Article> _articlesList;
        public ObservableCollection<Article> ArticlesList
        {
            get { return _articlesList; }
            set { SetProperty(ref _articlesList, value); }
        }

        public GestionStockViewModel()
        {
            SaveArticleCommand = new ViewModelCommand<Article>(ExecuteSaveArticleCommand);
            GetArticles();
        }

        private async void GetArticles()
        {
            var content = await ModeCommun.client.GetStringAsync("Article");
            ArticlesList = new ObservableCollection<Article>(JsonConvert.DeserializeObject<List<Article>>(content));
        }

        public ICommand SaveArticleCommand { get; }
        private async void ExecuteSaveArticleCommand(Article obj)
        {    
            var response = await ModeCommun.client.PutAsJsonAsync("article/" + obj.Id, obj);            
        }

    }
}
