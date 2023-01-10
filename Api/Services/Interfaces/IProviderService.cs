using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IProviderService
{
    Task<List<Provider>> GetProviders();
    Task<Provider?> GetId(int id);
    Task<List<Provider>> CreateProvider(Provider user);
    Task<List<Provider>?> UpdateProvider(int id, Provider user);
    Task<List<Provider>?> DeleteProvider(int id);
}