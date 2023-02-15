﻿using Api.Models;
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

        public ProductListViewModel ProductListDataContext;

        //public ProductListViewModel ProductListDataContext
        //{
        //    get { return _productListDataContext; }
        //    set { _productListDataContext = value; }
        //}

        private string _productName;

        public string ProductName
        {
            get
            {
                return _productName;
            }
            set
            {
                SetProperty(ref _productName, value);
            }
        }

        public MainWindowViewModel()
        {
            //ModeCommun.client = new HttpClient();
            //ModeCommun.client.BaseAddress = new Uri("https://localhost:44301/api/");
            //ModeCommun.client.DefaultRequestHeaders.Accept.Clear();
            //ModeCommun.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //ModeCommun.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);
            ProductListDataContext = new ProductListViewModel();
            ProductName = "Louis le bg";
            GetUsers();

        }

        private async void GetUsers()
        {
            var content = await ModeCommun.client.GetStringAsync("User");
            var users = JsonConvert.DeserializeObject<List<User>>(content);
        }


    }
}