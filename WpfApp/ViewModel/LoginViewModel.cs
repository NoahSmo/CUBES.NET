using Api.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private HttpClient client = new HttpClient();
        private User CurrentUser;
        private UserLogin _login_User = new UserLogin();
        private string _errorMessage;

        private bool _isViewVisible = true;



        private SecureString _password;



        #region "Property"

        public UserLogin login_User
        {
            get
            {
                return this._login_User;
            }
            set
            {
                SetProperty(ref this._login_User, value);
            }
        }

        public SecureString Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            { 
                SetProperty(ref _errorMessage , value); 
            }
        }

        public bool IsViewVisible
        {
            get { return _isViewVisible; }
            set 
            { 
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        #endregion

        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RemenberPasswordCommand { get; }

        public LoginViewModel()
        {
            client.BaseAddress = new Uri("https://localhost:44301/api/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(ExecuteRecoverPasswordCommand);
        }

        private async void ConnectUser()
        {
            //login_User.Password= Password;
            var response = await client.PostAsJsonAsync("Login/DesktopLogin", login_User);
            var users = await response.Content.ReadAsStringAsync();
            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                CurrentUser = JsonConvert.DeserializeObject<User>(users);
                IsViewVisible = false;
                OnPropertyChanged(nameof(IsViewVisible));
            }
            else
            {
                ErrorMessage = JsonConvert.DeserializeObject<String>(users);

            }

        }

        private void ExecuteRecoverPasswordCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if(string.IsNullOrWhiteSpace(login_User.Email) || login_User.Email.Length < 3|| login_User.Password== null|| login_User.Password.Length < 3)
                validData = false;
            else
                validData = true;
            return validData;
        }

        private void ExecuteLoginCommand(object obj)
        {
            ConnectUser();
            
        }
    }

}
