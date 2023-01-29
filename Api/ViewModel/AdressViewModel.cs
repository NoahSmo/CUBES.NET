using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class AddressViewModel
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? ZipCode { get; set; }
        public User User { get; set; }

        public AddressViewModel(Address address)
        {
            Street = address.Street;
            City = address.City;
            Country = address.Country;
            ZipCode = address.ZipCode;
            User = address.User;
        }
    }
}