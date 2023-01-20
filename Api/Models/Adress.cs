using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Address
    {
        public int Id { get; set; }
        
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        
        public int? UserId { get; set; }
        public User? User { get; set; }
        
        public int? DomainId { get; set; }
        public Domain? Domain { get; set; }
    }
}