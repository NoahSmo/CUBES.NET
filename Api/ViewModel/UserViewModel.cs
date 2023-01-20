namespace Api.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; }
    }
    
    public class UserDetailsViewModel
    {
        public int Id { get; set; }
        
        public string Username { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; }
        public int? Phone { get; set; }

        public string Role { get; set; } = "User";
    }
}