using Api.Models;
using Api.ViewModels;

namespace Api.Services;

public interface IAddressService
{
    Task<List<AddressViewModel>> GetAddresses();
    Task<AddressViewModel?> GetId(int id);
    Task<AddressViewModel> CreateAddress(Address address);
    Task<AddressViewModel>? UpdateAddress(int id, Address address);
    Task<AddressViewModel>? DeleteAddress(int id);
}
