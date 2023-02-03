using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class UserService : IUserService
{
    
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }
    
    public async Task<List<UserDetailsViewModel>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        
        return users.Select(user => new UserDetailsViewModel(user)).ToList();
    }

    public async Task<UserViewModel?> GetId(int id)
    { 
        var user = await _context.Users.FindAsync(id);
        
        if (user is null) return null;
        
        return new UserViewModel(user);
    }
    
    public async Task<UserViewModel?> GetByEmail (string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        
        if (user is null)
            return null;
        
        return new UserViewModel(user);
    }

    public async Task<UserViewModel> CreateUser(User user)
    {
        
        var password = user.Password;
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

        user.Email = user.Email.ToLower();
        user.Password = hash;
        
        _context.Users.Add(user);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return new UserViewModel(user);
    }
    
    public async Task<UserDetailsViewModel>? UpdateUser(int id, User request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null) return null;
        
        user.Name = request.Name;
        user.Surname = request.Surname;
        user.Email = request.Email.ToLower();
        user.Phone = request.Phone;
        user.RoleId = request.RoleId;
        user.Role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == request.RoleId);

        var password = request.Password;
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
        
        user.Password = hash;

        _context.Users.Update(user);
        
        await _context.SaveChangesAsync();

        return new UserDetailsViewModel(user);
    }

    public async Task<UserDetailsViewModel>? DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null) return null;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return new UserDetailsViewModel(user);
    }

    
}