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
    internal class DomainListViewModel : ViewModelBase
    {
        private ObservableCollection<Domain> _domainsList;

        private bool _visibilityCreateMenu;
        private bool _visibilityEditMenu;
        
        private Domain _selectDomain;

        #region "Property"

        public ObservableCollection<Domain> DomainsList
        {
            get { return _domainsList; }
            set {SetProperty(ref _domainsList , value); }
        }

        public bool VisibilityCreateMenu
        {
            get { return _visibilityCreateMenu; }
            set {SetProperty(ref _visibilityCreateMenu , value); }
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
        public ICommand CreateDomainCommand { get; }
        
        
        public ICommand ToggleEditMenu { get; }
        public ICommand SaveDomainCommand { get; }
        
        
        public ICommand DeleteDomainCommand { get; }

        public DomainListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Object>(ExecuteToggleAddMenu);
            CreateDomainCommand = new ViewModelCommand<Object>(ExecuteCreateDomainCommand);
            
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
            VisibilityCreateMenu = !VisibilityCreateMenu;
        }
        private async void ExecuteCreateDomainCommand(object obj)
        {
            var response = await ModeCommun.client.PostAsJsonAsync("domain", SelectDomain);
            VisibilityCreateMenu = !VisibilityCreateMenu;
            GetDomains();
        }

        
        private void ExecuteToggleEditMenu(Domain obj)
        {
            SelectDomain = obj;
            VisibilityEditMenu = !VisibilityEditMenu;
        }
        private async void ExecuteSaveDomainCommand(Object obj)
        {
            var response = await ModeCommun.client.PutAsJsonAsync("domain/" + SelectDomain.Id, SelectDomain);
            VisibilityEditMenu = !VisibilityEditMenu;
            GetDomains();
        }
        
        
        private async void DeleteDomain(Domain obj)
        {
            var response = await ModeCommun.client.DeleteAsync("domain/" + obj.Id);
            GetDomains();
        }
    }
}