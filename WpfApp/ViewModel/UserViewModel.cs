using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Api.Models;

namespace WpfApp.ViewModel
{
    internal class UserViewModel : ViewModelBase
    {
        private string _userMail;
        private string _userFirstName;
        private string _userLastName;
        private string _userRole;
        private int? _userPhone;

        #region "Properties"

        public string UserMail
        {
            get { return _userMail; }
            set { SetProperty(ref _userMail, value); }
        }

        public string UserFirstName
        {
            get { return _userFirstName; }
            set { SetProperty(ref _userFirstName, value); }
        }

        public string UserLastName
        {
            get { return _userLastName; }
            set { SetProperty(ref _userLastName, value); }
        }

        public string UserRole
        {
            get { return _userRole; }
            set { SetProperty(ref _userRole, value); }
        }

        public int? UserPhone
        {
            get { return _userPhone; }
            set { SetProperty(ref _userPhone, value); }
        }

        #endregion

        public UserViewModel()
        {
            UserMail = "john.doe@gmail.com";
            UserFirstName = "John";
            UserLastName = "Doe";
            UserRole = "Admin";
            UserPhone = 123456789;
        }

        public UserViewModel(UserData user)
        {
            UserMail = user.Email;
            UserFirstName = user.Name;
            UserLastName = user.Surname;
            UserRole = user.Role;
            UserPhone = user.Phone;
        }
    }
}