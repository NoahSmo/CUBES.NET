using System;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
    }
}
