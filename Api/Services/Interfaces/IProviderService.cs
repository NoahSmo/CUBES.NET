using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IProviderService
{
    Task<List<Provider>> GetProviders();
    Task<Provider?> GetId(int id);
    Task<Provider> CreateProvider(Provider user);
    Task<Provider>? UpdateProvider(int id, Provider user);
    Task<Provider>? DeleteProvider(int id);
}