using Api.Models;

namespace Api;

public interface IDomainAddressService
{
    Task<List<DomainAddress?>> GetDomainAddresses();
    Task<DomainAddress?> GetId(int id);
    Task<DomainAddress> CreateDomainAddress(DomainAddress domainAddress);
    Task<DomainAddress>? UpdateDomainAddress(int id, DomainAddress domainAddress);
    Task<DomainAddress>? DeleteDomainAddress(int id);
}