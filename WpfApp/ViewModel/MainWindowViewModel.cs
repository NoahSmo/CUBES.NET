﻿using Api.Models;
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

        
        private DomainListViewModel _domainListDataContext;
        public DomainListViewModel DomainListDataContext
        {
            get { return _domainListDataContext; }
            set { SetProperty(ref _domainListDataContext , value); }
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

        private Frame _currentView;
        public Frame CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView , value); }
        }
            
        
        
        public ICommand RedirectToArticleList { get; }
        public ICommand RedirectToDomainList { get; }
        
        

        public MainWindowViewModel()
        {
            //ModeCommun.client = new HttpClient();
            //ModeCommun.client.BaseAddress = new Uri("https://localhost:44301/api/");
            //ModeCommun.client.DefaultRequestHeaders.Accept.Clear();
            //ModeCommun.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //ModeCommun.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _accessToken);
            
            ArticleListDataContext = new ArticleListViewModel();
            DomainListDataContext = new DomainListViewModel();
            CategoryListDataContext = new CategoryListViewModel();
            UserListDataContext = new UserListViewModel();
            GestionStockDataContext = new GestionStockViewModel();

            RedirectToArticleList = new ViewModelCommand<Object>(ExecuteRedirectToArticleList);
            RedirectToDomainList = new ViewModelCommand<Object>(ExecuteRedirectToDomainList);
            
        }
        
        private void ExecuteRedirectToArticleList(Object obj)
        {
            ArticleListDataContext = new ArticleListViewModel();
            CurrentView.Navigate(new Uri("View/ArticleListView.xaml", UriKind.Relative));
        }
        
        private void ExecuteRedirectToDomainList(Object obj)
        {
            DomainListDataContext = new DomainListViewModel();
            CurrentView.Navigate(new Uri("View/DomainListView.xaml", UriKind.Relative));
        }
    }
}
