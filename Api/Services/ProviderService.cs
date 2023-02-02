using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ProviderService : IProviderService
{
    
    private readonly DataContext _context;

    public ProviderService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<Provider>> GetProviders()
    {
        var providers = await _context.Providers.ToListAsync();
        return providers;
    }

    public async Task<Provider?> GetId(int id)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider is null)
            return null;

        return provider;
    }
    
    public async Task<Provider> CreateProvider(Provider provider)
    {
        _context.Providers.Add(provider);
        await _context.SaveChangesAsync();
        return provider;
    }
    
    public async Task<Provider>? UpdateProvider(int id, Provider request)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider is null)
            return null;

        provider.Name = request.Name;
        provider.Email = request.Email;
        
        _context.Providers.Update(provider);
        await _context.SaveChangesAsync();

        return provider;
    }

    public async Task<Provider>? DeleteProvider(int id)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider is null)
            return null;

        _context.Providers.Remove(provider);
        await _context.SaveChangesAsync();

        return provider;
    }

    
}