using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Serial { get; set; }
        public virtual ICollection<Article>? Articles { get; set; }
    }
}
