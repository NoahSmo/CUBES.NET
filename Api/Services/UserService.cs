using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class UserService : IUserService
{
    
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<User>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        return users;
    }

    public async Task<User?> GetId(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        return user;
    }
    
    public async Task<List<User>> CreateUser(User user)
    {
        
        
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return await _context.Users.ToListAsync();
    }
    
    public async Task<List<User>?> UpdateUser(int id, User request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        user.Username = request.Username;
        user.Name = request.Name;
        user.Surname = request.Surname;
        user.Email = request.Email;
        user.Phone = request.Phone;
        user.Address = request.Address;
        user.City = request.City;
        user.Country = request.Country;
        user.PostCode = request.PostCode;
        user.Password = request.Password;
        user.IsAdmin = request.IsAdmin;

        await _context.SaveChangesAsync();

        return await _context.Users.ToListAsync();
    }

    public async Task<List<User>?> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return await _context.Users.ToListAsync();
    }

    
}