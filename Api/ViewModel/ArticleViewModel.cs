    
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
        public int Year { get; set; }
        public double Price { get; set; }
        public double Alcohol { get; set; }
        public int Stock { get; set; }
        
        
        public DomainViewModel Domain { get; set; }
        public CategoryViewModel Category { get; set; }
        public int CategoryId { get; set; }
        public int DomainId { get; set; }


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
            Domain = new DomainViewModel(article.Domain);
            Category = new CategoryViewModel(article.Category);
            CategoryId = article.CategoryId;
            DomainId = article.DomainId;
            
            Images = article.Images;
        }
    }

    public class ArticleOrderViewModel
    {
        public int ArticleId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
    }
}
