using Api.Models;
using Api.ViewModels;

namespace Api;

public interface IAddressService
{
    Task<List<Address>> GetAddresses();
    Task<AddressViewModel?> GetId(int id);
    Task<Address> CreateAddress(Address address);
    Task<Address>? UpdateAddress(int id, Address address);
    Task<Address>? DeleteAddress(int id);
}
