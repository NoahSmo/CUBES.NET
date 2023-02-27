using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class RoleService : IRoleService
{
    
    private readonly DataContext _context;

    public RoleService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<Role>> GetRoles()
    {
        var roles = await _context.Roles
            .Include(r => r.Permissions)
            .ToListAsync();
        return roles;
    }

    public async Task<Role?> GetId(int id)
    {
        var role = await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (role is null)
            return null;

        return role;
    }
    
    public async Task<Role> CreateRole(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }
    
    public async Task<Role>? UpdateRole(int id, Role request)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role is null)
            return null;

        role.Name = request.Name;
        role.Permissions = request.Permissions;
        
        _context.Roles.Update(role);
        await _context.SaveChangesAsync();

        return role;
    }

    public async Task<Role>? DeleteRole(int id)
    {
        var role = await _context.Roles
            .Include(r => r.Permissions)
            .FirstOrDefaultAsync(r => r.Id == id);
        if (role is null)
            return null;

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();

        return role;
    }

    
}