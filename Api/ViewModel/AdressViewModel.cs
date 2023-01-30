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
        public Domain Domain { get; set; }

        public AddressViewModel(Address address)
        {
            if (address.UserId != null)
            {
                Street = address.Street;
                City = address.City;
                Country = address.Country;
                ZipCode = address.ZipCode;
                User = address.User;
            }

            else
            {
                Street = address.Street;
                City = address.City;
                Country = address.Country;
                ZipCode = address.ZipCode;
                Domain = address.Domain;
            }
            
            
        }
    }
}