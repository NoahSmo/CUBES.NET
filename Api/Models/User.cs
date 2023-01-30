namespace Api.Models
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    
    public class User : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        public Role Role { get; set; }
        public string Password { get; set; }
        
        
        public virtual List<Address>? Addresses { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
        
        public virtual List<Comment>? Comments { get; set; }
    }
}