using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class UserDetails
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        
        public string Role { get; set; }
        
        public string Password { get; set; }

        public string HashPassword()
        {
            string salt = "a849qsd4165x146wxc436s1dqs32d4azq98d74q6d1azsdqs6d5qs4dq5s1d3qs6d47q5sd6wx1cwxc";
            
            return BCrypt.Net.BCrypt.HashPassword(Password + salt);            
        }
    }
}