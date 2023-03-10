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
        //private HttpClient client = new HttpClient();
        //private User CurrentUser;
        private UserLogin _login_User = new UserLogin();
        private string _errorMessage;

        private bool _isViewVisible = true;



        private SecureString _password;



        #region "Property"

        public UserLogin login_User
        {
            get { return _login_User; }
            set { SetProperty(ref _login_User, value); }
        }

        public SecureString Password
        {
            get { return _password; }
            set {  SetProperty(ref _password, value); }
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
            ModeCommun.client = new HttpClient();
            ModeCommun.client.BaseAddress = new Uri("https://localhost:44301/api/");
            ModeCommun.client.DefaultRequestHeaders.Accept.Clear();
            ModeCommun.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            LoginCommand = new ViewModelCommand<object>(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand<object>(ExecuteRecoverPasswordCommand);
        }

        private async void ConnectUser()
        {
            IntPtr passwordBSTR = IntPtr.Zero;
            try
            {
                passwordBSTR = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(Password);
                login_User.Password = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(passwordBSTR);
            }
            finally
            {
                if (passwordBSTR != IntPtr.Zero)
                {
                    System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(passwordBSTR);
                }
            }
            var response = await ModeCommun.client.PostAsJsonAsync("Login/DesktopLogin", login_User);
            var users = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ModeCommun.CurrentUser = JsonConvert.DeserializeObject<UserViewModel>(users);
                var responseToken = await ModeCommun.client.PostAsJsonAsync("Login", login_User);
                var tokenJson = await responseToken.Content.ReadAsStringAsync();
                
                var token = JsonConvert.DeserializeObject<ResponseData>(tokenJson);
                ModeCommun.client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token.value);
                
                IsViewVisible = false;
                OnPropertyChanged(nameof(IsViewVisible));
            }
            else
            {
                ErrorMessage = "Email or password incorrect";
            }

        }

        private void ExecuteRecoverPasswordCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if(string.IsNullOrWhiteSpace(login_User.Email) || login_User.Email.Length < 3|| Password== null|| Password.Length < 3)
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

    public class ResponseData
    {
        public string contentType { get; set; }
        public string serializerSettings { get; set; }
        public string statusCode { get; set; }
        public string value { get; set; }
    }


}

