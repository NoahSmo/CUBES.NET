using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;
using Api.ViewModels;

namespace Api.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Serial { get; set; } = string.Empty;
        public virtual List<ArticleOrder>? ArticleOrders { get; set; }
    }
}
