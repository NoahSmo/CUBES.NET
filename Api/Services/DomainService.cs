using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class DomainService : IDomainService
{
    
    private readonly DataContext _context;

    public DomainService(DataContext context)
    {
        _context = context;
    }


    public async Task<List<Domain?>> GetDomains()
    {
        return await _context.Domains.ToListAsync();
    }

    public async Task<Domain?> GetId(int id)
    {
        return await _context.Domains.FindAsync(id);
    }

    public async Task<Domain?> GetByName(string name)
    {
        return await _context.Domains.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<Domain> CreateDomain(Domain domain)
    {
        _context.Domains.Add(domain);
        await _context.SaveChangesAsync();

        return domain;
    }

    public async Task<Domain>? UpdateDomain(int id, Domain domain)
    {
        var domainToUpdate = await _context.Domains.FindAsync(id);

        if (domainToUpdate == null)
        {
            return null;
        }

        domainToUpdate.Name = domain.Name;
        domainToUpdate.Description = domain.Description;

        _context.Domains.Update(domainToUpdate);
        await _context.SaveChangesAsync();

        return domainToUpdate;
    }

    public async Task<Domain>? DeleteDomain(int id)
    {
        var domain = await _context.Domains.FindAsync(id);
        if (domain == null)
        {
            return null;
        }

        _context.Domains.Remove(domain);
        await _context.SaveChangesAsync();

        return domain;
    }
}