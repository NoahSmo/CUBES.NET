using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Image
    {
        public int Id { get; set; }
        
        public string Url { get; set; }
        
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}