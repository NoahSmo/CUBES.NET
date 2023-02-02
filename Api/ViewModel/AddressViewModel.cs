using Api.Models;

namespace Api.ViewModels
{
    public class AddressViewModel
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int? ZipCode { get; set; }
        public int? UserId { get; set; }
        public string User { get; set; }
        public int? DomainId { get; set; }
        public string Domain { get; set; }

        public AddressViewModel(Address address)
        {
            if (address.UserId != null)
            {
                Street = address.Street;
                City = address.City;
                Country = address.Country;
                ZipCode = address.ZipCode;
                UserId = address.UserId;
            }

            else if (address.DomainId != null)
            {
                Street = address.Street;
                City = address.City;
                Country = address.Country;
                ZipCode = address.ZipCode;
                DomainId = address.DomainId;
            }
            
            else
            {
                Street = address.Street;
                City = address.City;
                Country = address.Country;
                ZipCode = address.ZipCode;
            }
        }
    }
}