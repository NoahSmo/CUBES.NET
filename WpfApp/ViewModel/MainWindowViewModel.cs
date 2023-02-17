using Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WpfApp;
using Newtonsoft.Json;

namespace WpfApp.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {

        
        
        private string _accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImpvaG4uZG9lQGdtYWlsLmNvbSIsIm5hbWVpZCI6IjEiLCJyb2xlIjoiQWRtaW4iLCJQZXJtaXNzaW9uIjoiQ3JlYXRlLFJlYWQsVXBkYXRlLERlbGV0ZSIsIm5iZiI6MTY3NTQzMjc5MSwiZXhwIjoxNjc1NDMzMzkxLCJpYXQiOjE2NzU0MzI3OTEsImlzcyI6Ik5FR09TVUQiLCJhdWQiOiJVU0VSUyBBTkQgQURNSU5TIn0.907vkoTd8fmnzHyn1-TKVs84v1CMBPt4_N_a9Dpj6hA";
        private ArticleListViewModel _articleListDataContext;


        public ArticleListViewModel ArticleListDataContext
        {
            get { return _articleListDataContext; }
            set { SetProperty(ref _articleListDataContext , value); }
        }
        public UserViewModel UserDataContext;

        public MainWindowViewModel()
        {
            //ModeCommun.client = new HttpClient();
            //ModeCommun.client.BaseAddress = new Uri("https://localhost:44301/api/");
            //ModeCommun.client.DefaultRequestHeaders.Accept.Clear();
            //ModeCommun.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //ModeCommun.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);
            
            ArticleListDataContext = new ArticleListViewModel();
            UserDataContext = new UserViewModel();
            
        }
    }
}
