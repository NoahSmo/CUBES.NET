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
    internal class UserListViewModel : ViewModelBase
    {
        private ObservableCollection<User> _usersList;
        private List<Role> _rolesList;

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
        public ICommand ToggleEditMenu { get; }        
        
        public ICommand SaveUserCommand { get; }        
        
        public ICommand DeleteUserCommand { get; }

        public UserListViewModel()
        {
            ToggleAddMenu = new ViewModelCommand<Object>(ExecuteToggleAddMenu);
            ToggleEditMenu = new ViewModelCommand<User>(ExecuteToggleEditMenu);
            
            SaveUserCommand = new ViewModelCommand<User>(ExecuteSaveUserCommand);
            
            DeleteUserCommand = new ViewModelCommand<User>(DeleteUser);
            RefreshUser = new ViewModelCommand<object>(ExecuteRefreshUserCommand);
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
            VisibilityEditMenu = true;
        }

        
        private void ExecuteToggleEditMenu(User obj)
        {
            SelectUser = obj;
            VisibilityEditMenu = true;
        }
        public async void ExecuteSaveUserCommand(User obj)
        {
            if (SelectUser.Id == 0)
            {
                var response = await ModeCommun.client.PostAsJsonAsync("User", SelectUser);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    GetUsers();
                    VisibilityEditMenu = false;
                }

            }
            else
            {
                SelectUser.Role = null;
                var response = await ModeCommun.client.PutAsJsonAsync("User/" + SelectUser.Id, SelectUser);
                GetUsers();
                VisibilityEditMenu = false;
            }
        }
        
        
        private async void DeleteUser(User obj)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                var response = await ModeCommun.client.DeleteAsync("User/" + obj.Id);
                UsersList.Remove(obj);
            }
        }

        public ICommand RefreshUser { get; }
        public async void ExecuteRefreshUserCommand(object obj)
        {
            GetUsers();
            GetRoles();
        }
    }
}