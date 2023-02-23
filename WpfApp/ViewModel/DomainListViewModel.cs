using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Api.Models;
using Newtonsoft.Json;

namespace WpfApp.ViewModel
{
    internal class DomainListViewModel : ViewModelBase
    {
        private ObservableCollection<Domain> _domainsList;

        private bool _visibilityEditMenu;
        
        private Domain _selectDomain;

        #region "Property"

        public ObservableCollection<Domain> DomainsList
        {
            get { return _domainsList; }
            set {SetProperty(ref _domainsList , value); }
        }

        public bool VisibilityEditMenu
        {
            get { return _visibilityEditMenu; }
            set {SetProperty(ref _visibilityEditMenu , value); }
        }
        
        public Domain SelectDomain
        {
            get { return _selectDomain; }
            set {SetProperty(ref _selectDomain , value); }
        }

        #endregion
        
        public ICommand ToggleAddMenu { get; }
        public ICommand ToggleEditMenu { get; }
                
        public ICommand SaveDomainCommand { get; }
            
        public ICommand DeleteDomainCommand { get; }

        public DomainListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Object>(ExecuteToggleAddMenu);
            ToggleEditMenu = new ViewModelCommand<Domain>(ExecuteToggleEditMenu);
            
            SaveDomainCommand = new ViewModelCommand<Object>(ExecuteSaveDomainCommand);
            
            DeleteDomainCommand = new ViewModelCommand<Domain>(DeleteDomain);
            
            GetDomains();
        }
        

        private async void GetDomains()
        {
            var content = await ModeCommun.client.GetStringAsync("Domain");
            DomainsList = new ObservableCollection<Domain>( JsonConvert.DeserializeObject<List<Domain>>(content));
        }
        
        
        private void ExecuteToggleAddMenu(Object obj)
        {
            SelectDomain = new Domain();
            VisibilityEditMenu = true;
        }

        private void ExecuteToggleEditMenu(Domain obj)
        {
            SelectDomain = obj;
            VisibilityEditMenu = true;
        }
        private async void ExecuteSaveDomainCommand(Object obj)
        {
            if (SelectDomain.Id == 0)
            {
                var response = await ModeCommun.client.PostAsJsonAsync("domain", SelectDomain);                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetDomains();
                    VisibilityEditMenu = false;
                }

            }
            else
            {
                var response = await ModeCommun.client.PutAsJsonAsync("domain/" + SelectDomain.Id, SelectDomain);
            }
        }
        
        
        private async void DeleteDomain(Domain obj)
        {
            if (MessageBox.Show("Are you sure you want to delete this domain?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {                
                var response = await ModeCommun.client.DeleteAsync("domain/" + obj.Id);
                DomainsList.Remove(obj);
            }
        }
    }
}