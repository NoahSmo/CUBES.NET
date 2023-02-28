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
        private Address _selectAddress;

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
        
        public Address SelectAddress
        {
            get { return _selectAddress; }
            set {SetProperty(ref _selectAddress , value); }
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
            
            SaveProviderCommand = new ViewModelCommand<Provider>(ExecuteSaveProviderCommand);
            
            DeleteProviderCommand = new ViewModelCommand<Provider>(DeleteProvider);
            RefreshProvider = new ViewModelCommand<object>(ExecuteRefreshProviderCommand);
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
            SelectAddress = new Address();
            VisibilityEditMenu = true;
        }

        private void ExecuteToggleEditMenu(Provider obj)
        {
            SelectProvider = obj;
            SelectAddress = obj.Address;
            VisibilityEditMenu = true;
        }
        private async void ExecuteSaveProviderCommand(Provider obj)
        {
            
            if (SelectProvider.Id == 0)
            {
                var responseAddress = await ModeCommun.client.PostAsJsonAsync("address", SelectAddress);
                if (responseAddress.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await responseAddress.Content.ReadAsStringAsync();
                    SelectAddress = JsonConvert.DeserializeObject<Address>(content);
                    
                    SelectProvider.AddressId = SelectAddress.Id;
                    SelectProvider.Address = null;
                    
                    var response = await ModeCommun.client.PostAsJsonAsync("provider", SelectProvider);                
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        GetProviders();
                        VisibilityEditMenu = false;
                    }
                }
                
                GetProviders();
                VisibilityEditMenu = false;
            }
            else
            {
                var responseAddress = await ModeCommun.client.PutAsJsonAsync("address/" + SelectAddress.Id, SelectAddress);
                if (responseAddress.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var content = await responseAddress.Content.ReadAsStringAsync();
                    SelectAddress = JsonConvert.DeserializeObject<Address>(content);
                    
                    SelectProvider.AddressId = SelectAddress.Id;
                    SelectProvider.Address = null;
                    
                    var response = await ModeCommun.client.PutAsJsonAsync("provider/" + SelectProvider.Id, SelectProvider);             
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        GetProviders();
                        VisibilityEditMenu = false;
                    }
                }
                
                GetProviders();
                VisibilityEditMenu = false;
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

        public ICommand RefreshProvider { get; }
        public async void ExecuteRefreshProviderCommand(object obj)
        {
            GetProviders();
        }

    }
}