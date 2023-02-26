using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Provider : Auditable
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        
        public virtual List<Article>? Articles { get; set; }
    }
}
