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
        
        public int AddressId { get; set; }
        public Address? Address { get; set; }

        public ProviderViewModel(Provider provider)
        {
            Id = provider.Id;
            Name = provider.Name;
            Email = provider.Email;
            AddressId = provider.AddressId;
            Address = provider.Address;
        }
    }
}
