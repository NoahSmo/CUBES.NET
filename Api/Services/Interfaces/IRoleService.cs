using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IRoleService
{
    Task<List<Role?>> GetRoles();
    Task<Role?> GetId(int id);
    Task<Role> CreateRole(Role role);
    Task<Role>? UpdateRole(int id, Role role);
    Task<Role>? DeleteRole(int id);
}