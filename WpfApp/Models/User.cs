using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;
using System.Drawing;
using System.Runtime.CompilerServices;
using WpfApp.ViewModel;

namespace Api.Models
{
    public class UserLogin : ViewModelBase
    {

        private string _email;
        public string Email
        {
            get
            {
                return this._email;
            }
            set
            {
                SetProperty(ref this._email, value);
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                SetProperty(ref this._password, value);
            }
        }
    }



    public class User : ViewModelBase
    {
        public int Id { get; set; }

        private string _userName;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Username
        {
            get
            {
                return this._userName;
            }
            set 
            {
                SetProperty(ref this._userName , value);
            }
        }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Phone { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;

        public bool IsAdmin { get; set; }

        public string Password { get; set; }

        public virtual List<Order>? Orders { get; set; }
    }
}