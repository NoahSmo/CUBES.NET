using System;

namespace Api
{
    public class UserData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    
    public class UserDetailsData
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
        
        public string Password { get; set; }
        
        public string Role { get; set; }
        
        
        public Result result { get; set; }
    }

    public class Result
    {
        public bool result { get; set; }
        public string message { get; set; }
    }
}
