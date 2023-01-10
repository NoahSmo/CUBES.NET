using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Year { get; set; }
        public string Price { get; set; }
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int Stock { get; set; }
    }
}
