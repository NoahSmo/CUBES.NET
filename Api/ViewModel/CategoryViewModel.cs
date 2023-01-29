using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        
        public CategoryViewModel(Category category)
        {
            Name = category.Name;
            Description = category.Description;
        }
    }
}
