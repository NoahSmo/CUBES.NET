using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class AddressService : IAddressService
{
    
    private readonly DataContext _context;

    public AddressService(DataContext context)
    {
        _context = context;
    }


    public async Task<List<AddressViewModel>> GetAddresses()
    {
        var address = await _context.Addresses
            .Include(a => a.User)
            .Include(a => a.Domain)
            .ToListAsync();
        
        return address.Select(a => new AddressViewModel(a)).ToList();
    }

    public async Task<AddressViewModel?> GetId(int id)
    {
        var address = _context.Addresses
            .Include(a => a.User)
            .Include(a => a.Domain)
            .FirstOrDefault(a => a.Id == id);
        
        if (address == null) return null;
        
        return new AddressViewModel(address);
    }

    public async Task<AddressViewModel>? CreateAddress(Address address)
    {
        if (address.UserId != null)
        {
            address.User = _context.Users.FirstOrDefault(x => x.Id == address.UserId);
        }
        else if (address.DomainId != null)
        {
            address.Domain = _context.Domains.FirstOrDefault(x => x.Id == address.DomainId);
        }
        
        _context.Addresses.Add(address);

        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return new AddressViewModel(address);
    }

    public async Task<AddressViewModel>? UpdateAddress(int id, Address request)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null) return null;
        
        address.Street = request.Street;
        address.City = request.City;
        address.Country = request.Country;
        address.ZipCode = request.ZipCode;
        
        if (request.UserId != null)
        {
            address.UserId = request.UserId;
            address.User = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
            
            address.DomainId = null;
            address.Domain = null;
        }
        else if (request.DomainId != null)
        {
            address.DomainId = request.DomainId;
            address.Domain = _context.Domains.FirstOrDefault(x => x.Id == request.DomainId);
            
            address.UserId = null;
            address.User = null;
        }
        
        _context.Addresses.Update(address);
        
        await _context.SaveChangesAsync();

        return new AddressViewModel(address);
    }

    public async Task<AddressViewModel>? DeleteAddress(int id)
    {
        var addressToDelete = await _context.Addresses.FindAsync(id);
        if (addressToDelete == null) return null;

        _context.Addresses.Remove(addressToDelete);
        await _context.SaveChangesAsync();
        
        return new AddressViewModel(addressToDelete);
    }
}