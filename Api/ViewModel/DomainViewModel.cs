using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class DomainViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        private List<DomainAddress>? Address { get; set; }
        
        
        
        public DomainViewModel(Domain domain)
        {
            Name = domain.Name;
            Description = domain.Description;
            Address = domain.DomainAddresses;
        }
    }
    
    
}
