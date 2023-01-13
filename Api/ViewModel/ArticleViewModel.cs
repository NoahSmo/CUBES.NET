using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Year { get; set; }
        public string Price { get; set; }
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int Stock { get; set; }
        
        public virtual List<ArticleOrder>? ArticleOrders { get; set; }
    }

    public class ArticleOrderViewModel
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}