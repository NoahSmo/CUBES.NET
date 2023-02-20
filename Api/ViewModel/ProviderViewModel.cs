using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ProviderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        public virtual List<Address?> Addresses { get; set; }

        public ProviderViewModel(Provider provider)
        {
            Id = provider.Id;
            Name = provider.Name;
            Email = provider.Email;
            Addresses = provider.Addresses;
        }
    }
}
