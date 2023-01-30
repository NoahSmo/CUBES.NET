using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ArticleViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Price { get; set; }
        public double Alcohol { get; set; }
        public int Stock { get; set; }
        
        public Domain Domain { get; set; }
        
        public Category Category { get; set; }
        
        public virtual List<ArticleOrder>? ArticleOrders { get; set; }
        public virtual List<Provider>? Providers { get; set; }
        public virtual List<Image>? Images { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        
        public ArticleViewModel(Article article)
        {
            Name = article.Name;
            Description = article.Description;
            Year = article.Year;
            Price = article.Price;
            Alcohol = article.Alcohol;
            Stock = article.Stock;
            Domain = article.Domain;
            Category = article.Category;
            
            ArticleOrders = article.ArticleOrders;
            Providers = article.Providers;
            Images = article.Images;
            Comments = article.Comments;
        }
    }

    public class ArticleOrderViewModel
    {
        public int ArticleId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
