using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; }
        public string Phone { get; set; } = string.Empty;
        
        public string Role { get; set; } = "User";
        
        public string Password { get; set; }
        
        public virtual List<Address>? Addresses { get; set; }
        
        public virtual List<Order>? Orders { get; set; }
    }

    public class ArticleOrderViewModel
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
