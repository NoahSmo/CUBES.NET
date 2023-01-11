using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{


    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; }
        public int? Phone { get; set; }
        
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostCode { get; set; } = string.Empty;
        
        public bool IsAdmin { get; set; }
        
        public string Password { get; set; }

        public string HashPassword()
        {
            string salt = "a849qsd4165x146wxc436s1dqs32d4azq98d74q6d1azsdqs6d5qs4dq5s1d3qs6d47q5sd6wx1cwxc";
            
            return BCrypt.Net.BCrypt.HashPassword(Password + salt);            
        }
    }
}