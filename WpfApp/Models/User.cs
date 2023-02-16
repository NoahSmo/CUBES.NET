using System.Collections.Generic;

namespace Api.Models
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class UserData
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? Phone { get; set; }
        public string Role { get; set; }
    }
    
    public class User : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        
        public int RoleId { get; set; } = 2;
        public Role? Role { get; set; }
        
        public int? CartId { get; set; }
        public Cart? Cart { get; set; }
        
        public string Password { get; set; }
        

        public virtual List<Address>? Addresses { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
        
        public virtual List<Comment>? Comments { get; set; }
    }
}