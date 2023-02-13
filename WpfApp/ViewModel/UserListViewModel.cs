using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewModel
{
    internal class UserListViewModel: ViewModelBase
    {

        private string _userMail;
        private string _userFirstName;
        private string _userLastName;
        private string _userPassword;
        private string _userRole;
        
        public string UserMail
        {
            get
            {
                return _userMail;
            }
            set
            {
                SetProperty(ref _userMail, value);
            }
        }
        
        public string UserFirstName
        {
            get
            {
                return _userFirstName;
            }
            set
            {
                SetProperty(ref _userFirstName, value);
            }
        }
        
        public string UserLastName
        {
            get
            {
                return _userLastName;
            }
            set
            {
                SetProperty(ref _userLastName, value);
            }
        }
        
        public string UserRole
        {
            get
            {
                return _userRole;
            }
            set
            {
                SetProperty(ref _userRole, value);
            }
        }
        
        
        public UserListViewModel()
        {
            UserMail = "john.doe@gmail.com";
            UserFirstName = "John";
            UserLastName = "Doe";
            UserRole = "Admin";
        }
    }
}
