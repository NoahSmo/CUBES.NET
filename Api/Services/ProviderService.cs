using System.Collections.Generic;
using System.Threading.Tasks;
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
        var providers = await _context.Providers
            .Include(p => p.Address)
            .ToListAsync();
        return providers;
    }

    public async Task<Provider?> GetId(int id)
    {
        var provider = await _context.Providers
            .Include(p => p.Address)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (provider is null)
            return null;

        return provider;
    }
    
    public async Task<Provider> CreateProvider(Provider provider)
    {
        provider.Id = _context.Providers.Max(p => p.Id) + 1;
        
        provider.Address = await _context.Addresses.FindAsync(provider.AddressId);
        
        _context.Providers.Add(provider);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return provider;
    }
    
    public async Task<Provider>? UpdateProvider(int id, Provider request)
    {
        var provider = await _context.Providers.FindAsync(id);
        if (provider is null) return null;

        provider.Name = request.Name;
        provider.Description = request.Description;
        provider.Email = request.Email;
        provider.AddressId = request.AddressId;
        provider.Address = await _context.Addresses.FindAsync(request.AddressId);
        provider.Articles = await _context.Articles.Where(a => a.ProviderId == provider.Id).ToListAsync();
        
        _context.Providers.Update(provider);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }

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