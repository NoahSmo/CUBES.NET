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
        public string Phone { get; set; } = string.Empty;
        
        public bool IsAdmin { get; set; }
        
        public string Password { get; set; }
        
        public virtual List<Address>? Addresses { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
    }
}