using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public Double Price { get; set; }
        public Double Alcohol { get; set; }
        public int Stock { get; set; }
        
        
        public int DomainId { get; set; }
        public Domain? Domain { get; set; }
        
        
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        
        public virtual List<ArticleOrder>? ArticleOrders { get; set; }
        public virtual List<Provider>? Providers { get; set; }
        public virtual List<Image>? Images { get; set; }
        public virtual List<Comment>? Comments { get; set; }
    }

    public class ArticleOrder
    {
        public int Id { get; set; }
        public int ArticleId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
