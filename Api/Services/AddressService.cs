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


    public Task<List<AddressViewModel>> GetAddresses()
    {
        return _context.Addresses.Select(a => new AddressViewModel(a)).ToListAsync();
    }

    public Task<AddressViewModel?> GetId(int id)
    {
        var address = _context.Addresses.FirstOrDefault(x => x.Id == id);
        if (address == null) return null;
        return Task.FromResult(new AddressViewModel(address));
    }

    public Task<AddressViewModel> CreateAddress(Address address)
    {
        address.User = _context.Users.FirstOrDefault(x => x.Id == address.UserId);
        
        _context.Addresses.Add(address);
        _context.SaveChanges();
        return Task.FromResult(new AddressViewModel(address));
    }

    public Task<AddressViewModel>? UpdateAddress(int id, Address address)
    {
        var addressToUpdate = _context.Addresses.FirstOrDefault(x => x.Id == id);
        if (addressToUpdate == null) return null;
        
        addressToUpdate.Street = address.Street;
        addressToUpdate.City = address.City;
        addressToUpdate.Country = address.Country;
        addressToUpdate.ZipCode = address.ZipCode;
        addressToUpdate.UserId = address.UserId;
        addressToUpdate.User = _context.Users.FirstOrDefault(x => x.Id == address.UserId);
        addressToUpdate.DomainId = address.DomainId;
        addressToUpdate.Domain = _context.Domains.FirstOrDefault(x => x.Id == address.DomainId);
        
        _context.Addresses.Update(addressToUpdate);
        _context.SaveChanges();

        return Task.FromResult(new AddressViewModel(addressToUpdate));
    }

    public Task<AddressViewModel>? DeleteAddress(int id)
    {
        var addressToDelete = _context.Addresses.FirstOrDefault(x => x.Id == id);
        if (addressToDelete == null) return null;

        _context.Addresses.Remove(addressToDelete);
        _context.SaveChanges();

        return Task.FromResult(new AddressViewModel(addressToDelete));
    }
}