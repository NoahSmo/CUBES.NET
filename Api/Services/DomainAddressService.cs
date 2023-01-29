using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class DomainAddressService : IDomainAddressService
{
    
    private readonly DataContext _context;

    public DomainAddressService(DataContext context)
    {
        _context = context;
    }


    public async Task<List<DomainAddress?>> GetDomainAddresses()
    {
        return await _context.DomainAddresses.ToListAsync();
    }

    public async Task<DomainAddress?> GetId(int id)
    {
        return await _context.DomainAddresses.FindAsync(id);
    }

    public async Task<DomainAddress> CreateDomainAddress(DomainAddress domainAddress)
    {
        _context.DomainAddresses.Add(domainAddress);
        await _context.SaveChangesAsync();

        return domainAddress;
    }

    public async Task<DomainAddress>? UpdateDomainAddress(int id, DomainAddress domain)
    {
        var domainToUpdate = await _context.DomainAddresses.FindAsync(id);

        if (domainToUpdate == null)
        {
            return null;
        }
        
        domainToUpdate.Street = domain.Street;
        domainToUpdate.City = domain.City;
        domainToUpdate.Country = domain.Country;
        domainToUpdate.ZipCode = domain.ZipCode;
        domainToUpdate.DomainId = domain.DomainId;
        domainToUpdate.Domain = await _context.Domains.FindAsync(domain.DomainId);

        _context.DomainAddresses.Update(domainToUpdate);
        await _context.SaveChangesAsync();

        return domainToUpdate;
    }

    public async Task<DomainAddress>? DeleteDomainAddress(int id)
    {
        var domainAddress = await _context.DomainAddresses.FindAsync(id);
        if (domainAddress == null)
        {
            return null;
        }

        _context.DomainAddresses.Remove(domainAddress);
        await _context.SaveChangesAsync();

        return domainAddress;
    }
}