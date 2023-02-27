using Api.Models;

namespace Api.ViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? ZipCode { get; set; }
        public int? UserId { get; set; }
        public string User { get; set; }
        public int? ProviderId { get; set; }
        public string Provider { get; set; }

        public AddressViewModel(Address address)
        {
            if (address.UserId != null)
            {
                Id = address.Id;
                Street = address.Street;
                City = address.City;
                Country = address.Country;
                ZipCode = address.ZipCode;
                UserId = address.UserId;
                User =address.User.Email; 
            }

            else if (address.ProviderId != null)
            {
                Id = address.Id;
                Street = address.Street;
                City = address.City;
                Country = address.Country;
                ZipCode = address.ZipCode;
                ProviderId = address.ProviderId;
                Provider = address.Provider.Name;
            }
            
            else
            {
                Id = address.Id;
                Street = address.Street;
                City = address.City;
                Country = address.Country;
                ZipCode = address.ZipCode;
            }
        }
    }
}