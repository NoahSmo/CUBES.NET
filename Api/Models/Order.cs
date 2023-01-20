using System;
using System.ComponentModel.DataAnnotations;
using Api.ViewModels;

namespace Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User? User { get; set; }
        
        public DateTime Date { get; set; }
        
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        
        public int StatusId { get; set; }
        public Status? Status { get; set; }
        
        public virtual List<ArticleOrder>? ArticleOrders { get; set; }
    }
}
