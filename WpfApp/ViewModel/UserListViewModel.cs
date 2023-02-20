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
    internal class UserListViewModel : ViewModelBase
    {
        private ObservableCollection<User> _usersList;
        private List<Role> _rolesList;

        private bool _visibilityCreateMenu;
        private bool _visibilityEditMenu;
        
        private User _selectUser;

        #region "Property"

        public ObservableCollection<User> UsersList
        {
            get { return _usersList; }
            set {SetProperty(ref _usersList , value); }
        }
        
        public List<Role> RolesList
        {
            get { return _rolesList; }
            set {SetProperty(ref _rolesList , value); }
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
        
        public User SelectUser
        {
            get { return _selectUser; }
            set {SetProperty(ref _selectUser , value); }
        }

        #endregion
        
        public ICommand ToggleAddMenu { get; }
        public ICommand CreateUserCommand { get; }
        
        
        public ICommand ToggleEditMenu { get; }
        public ICommand SaveUserCommand { get; }
        
        
        public ICommand DeleteUserCommand { get; }

        public UserListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Object>(ExecuteToggleAddMenu);
            CreateUserCommand = new ViewModelCommand<User>(ExecuteCreateUserCommand);
            
            ToggleEditMenu = new ViewModelCommand<User>(ExecuteToggleEditMenu);
            SaveUserCommand = new ViewModelCommand<User>(ExecuteSaveUserCommand);
            
            DeleteUserCommand = new ViewModelCommand<User>(DeleteUser);
            
            GetUsers();

            GetRoles();
        }
        

        private async void GetUsers()
        {
            var content = await ModeCommun.client.GetStringAsync("User/wpf");
            UsersList = new ObservableCollection<User>( JsonConvert.DeserializeObject<List<User>>(content));
        }

        private async void GetRoles()
        {
            var content = await ModeCommun.client.GetStringAsync("Role");
            RolesList = new List<Role>( JsonConvert.DeserializeObject<List<Role>>(content));
        }
        
        
        private void ExecuteToggleAddMenu(Object obj)
        {
            SelectUser = new User();
            VisibilityCreateMenu = !VisibilityCreateMenu;
        }
        private async void ExecuteCreateUserCommand(User obj)
        {
            var response = await ModeCommun.client.PostAsJsonAsync("User", SelectUser);
            VisibilityCreateMenu = !VisibilityCreateMenu;
            GetUsers();
        }

        
        private void ExecuteToggleEditMenu(User obj)
        {
            SelectUser = obj;
            VisibilityEditMenu = !VisibilityEditMenu;
        }
        private async void ExecuteSaveUserCommand(User obj)
        {
            var response = await ModeCommun.client.PutAsJsonAsync("User/" + SelectUser.Id, obj);
            VisibilityEditMenu = !VisibilityEditMenu;
            GetUsers();
        }
        
        
        private async void DeleteUser(User obj)
        {
            var response = await ModeCommun.client.DeleteAsync("User/" + obj.Id);
            GetUsers();
        }
    }
}