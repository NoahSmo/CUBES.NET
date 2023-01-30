using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IProviderService
{
    Task<List<Provider>> GetProviders();
    Task<Provider?> GetId(int id);
    Task<Provider> CreateProvider(Provider provider);
    Task<Provider>? UpdateProvider(int id, Provider provider);
    Task<Provider>? DeleteProvider(int id);
}