using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Article : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Price { get; set; }
        public double Alcohol { get; set; }
        public int Stock { get; set; }

        public bool AutoRestock { get; set; } = true;
        
        
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }
        
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        
        
        public virtual List<ArticleOrder>? ArticleOrders { get; set; }
        public virtual List<CartItem>? CartItems { get; set; }
        public virtual List<Image>? Images { get; set; }
        public virtual List<Comment>? Comments { get; set; }
    }

    public class ArticleOrder
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
        public int? OrderId { get; set; }
        public int? ProviderOrderId { get; set; }
        public int Quantity { get; set; }
    }
}
