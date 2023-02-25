using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Provider : Auditable
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Email { get; set; }
        
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        
        public virtual List<Domain> Domains { get; set; }
        
        public virtual List<Article> Articles { get; set; }
    }
}
