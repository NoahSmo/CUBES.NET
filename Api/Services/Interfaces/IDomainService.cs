using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IDomainService
{
    Task<List<Domain?>> GetDomains();
    Task<Domain?> GetId(int id);
    Task<Domain?> GetByName(string name);
    Task<Domain> CreateDomain(Domain domain);
    Task<Domain>? UpdateDomain(int id, Domain domain);
    Task<Domain>? DeleteDomain(int id);
}