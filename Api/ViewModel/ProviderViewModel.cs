using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ProviderViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }

        public ProviderViewModel(Provider provider)
        {
            Name = provider.Name;
            Email = provider.Email;
            Phone = provider.Phone;
        }
    }
}
