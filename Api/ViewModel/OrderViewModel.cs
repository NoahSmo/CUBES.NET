using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;
using Api.ViewModels;

namespace Api.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        
        public int UserId { get; set; }
        
        public int AddressId { get; set; }
        
        public int StatusId { get; set; }
        
        public virtual List<ArticleOrder>? ArticleOrders { get; set; }
        
        public OrderViewModel(Order order)
        {
            Id = order.Id;
            Date = order.Date;
            UserId = order.UserId;
            AddressId = order.AddressId;
            StatusId = order.StatusId;
            ArticleOrders = order.ArticleOrders;
        }
    }
}
