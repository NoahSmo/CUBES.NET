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
        public int Id_Provider { get; set; }
        public int Id_Category { get; set; }
        public int Stock { get; set; }
    }
}
