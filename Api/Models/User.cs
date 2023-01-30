using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    // public enum Role { Admin, Provider, User }
    
    
    
    public class User : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; }
        public int? Phone { get; set; }
        
        // public string Role { get; set; } = "User";
        public Role Role { get; set; }
        public string Password { get; set; }
        
        
        public virtual List<Address>? Addresses { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
        
        public virtual List<Comment>? Comments { get; set; }
    }
}