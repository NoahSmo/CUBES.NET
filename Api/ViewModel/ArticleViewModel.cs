    
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Year { get; set; }
        public double Price { get; set; }
        public double Alcohol { get; set; }
        public int Stock { get; set; }
        
        public int? ProviderId { get; set; }
        public Provider Provider { get; set; }
        
        public int? CategoryId { get; set; }
        public CategoryViewModel Category { get; set; }

        public bool AutoRestock { get; set; }
        public virtual List<Image>? Images { get; set; }
        
        public ArticleViewModel(Article article)
        {
            Id = article.Id;
            Name = article.Name;
            Description = article.Description;
            Year = article.Year;
            Price = article.Price;
            Alcohol = article.Alcohol;
            Stock = article.Stock;
            AutoRestock = article.AutoRestock;

            ProviderId = article.ProviderId;
            Provider = article.Provider;

            CategoryId = article.CategoryId;
            Category = new CategoryViewModel(article.Category);
            
            Images = article.Images;
        }
    }

    public class ArticleOrderViewModel
    {
        public int ArticleId { get; set; }
        public Article? Article { get; set; }
        public int? OrderId { get; set; }
        public int? ProviderOrderId { get; set; }
        public int Quantity { get; set; }
    }
}
