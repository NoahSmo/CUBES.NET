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
    internal class ProviderListViewModel : ViewModelBase
    {
        private ObservableCollection<Provider> _providersList;

        private bool _visibilityEditMenu;
        
        private Provider _selectProvider;

        #region "Property"

        public ObservableCollection<Provider> ProvidersList
        {
            get { return _providersList; }
            set {SetProperty(ref _providersList , value); }
        }

        public bool VisibilityEditMenu
        {
            get { return _visibilityEditMenu; }
            set {SetProperty(ref _visibilityEditMenu , value); }
        }
        
        public Provider SelectProvider
        {
            get { return _selectProvider; }
            set {SetProperty(ref _selectProvider , value); }
        }

        #endregion
        
        public ICommand ToggleAddMenu { get; }
        public ICommand ToggleEditMenu { get; }
                
        public ICommand SaveProviderCommand { get; }
            
        public ICommand DeleteProviderCommand { get; }

        public ProviderListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Object>(ExecuteToggleAddMenu);
            ToggleEditMenu = new ViewModelCommand<Provider>(ExecuteToggleEditMenu);
            
            SaveProviderCommand = new ViewModelCommand<Object>(ExecuteSaveProviderCommand);
            
            DeleteProviderCommand = new ViewModelCommand<Provider>(DeleteProvider);
            
            GetProviders();
        }
        

        private async void GetProviders()
        {
            var content = await ModeCommun.client.GetStringAsync("Provider");
            ProvidersList = new ObservableCollection<Provider>( JsonConvert.DeserializeObject<List<Provider>>(content));
        }
        
        
        private void ExecuteToggleAddMenu(Object obj)
        {
            SelectProvider = new Provider();
            VisibilityEditMenu = true;
        }

        private void ExecuteToggleEditMenu(Provider obj)
        {
            SelectProvider = obj;
            VisibilityEditMenu = true;
        }
        private async void ExecuteSaveProviderCommand(Object obj)
        {
            if (SelectProvider.Id == 0)
            {
                var response = await ModeCommun.client.PostAsJsonAsync("provider", SelectProvider);                
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetProviders();
                    VisibilityEditMenu = false;
                }

            }
            else
            {
                var response = await ModeCommun.client.PutAsJsonAsync("provider/" + SelectProvider.Id, SelectProvider);
            }
        }
        
        
        private async void DeleteProvider(Provider obj)
        {
            if (MessageBox.Show("Are you sure you want to delete this provider?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {                
                var response = await ModeCommun.client.DeleteAsync("provider/" + obj.Id);
                ProvidersList.Remove(obj);
            }
        }
    }
}