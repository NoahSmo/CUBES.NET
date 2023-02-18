using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class DomainViewModel
    {
        public int Id { get; set; }
   
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        
        public DomainViewModel(Domain domain)
        {
            Id = domain.Id;
            Name = domain.Name;
            Description = domain.Description;
            Email = domain.Email;
        }
    }
    
    
}
