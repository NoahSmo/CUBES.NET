using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class AddressService : IAddressService
{
    
    private readonly DataContext _context;

    public AddressService(DataContext context)
    {
        _context = context;
    }


    public Task<List<Address>> GetAddresses()
    {
        return _context.Addresses.ToListAsync();
    }

    public Task<AddressViewModel?> GetId(int id)
    {
        return _context.Addresses.Where(x => x.Id == id).Select(x => new AddressViewModel
        {
            Street = x.Street,
            City = x.City,
            Country = x.Country,
            ZipCode = x.ZipCode
        }).FirstOrDefaultAsync();
    }

    public Task<Address> CreateAddress(Address address)
    {
        address.User = _context.Users.FirstOrDefault(x => x.Id == address.UserId);
        
        _context.Addresses.Add(address);
        _context.SaveChanges();
        return Task.FromResult(address);
    }

    public Task<Address>? UpdateAddress(int id, Address address)
    {
        var addressToUpdate = _context.Addresses.FirstOrDefault(x => x.Id == id);
        if (addressToUpdate == null) return null;
        
        addressToUpdate.Street = address.Street;
        addressToUpdate.City = address.City;
        addressToUpdate.Country = address.Country;
        addressToUpdate.ZipCode = address.ZipCode;
        
        _context.Addresses.Update(addressToUpdate);
        _context.SaveChanges();
        
        return Task.FromResult(addressToUpdate);
    }

    public Task<Address>? DeleteAddress(int id)
    {
        var addressToDelete = _context.Addresses.FirstOrDefault(x => x.Id == id);
        if (addressToDelete == null) return null;

        _context.Addresses.Remove(addressToDelete);
        _context.SaveChanges();
        
        return Task.FromResult(addressToDelete);
    }
}