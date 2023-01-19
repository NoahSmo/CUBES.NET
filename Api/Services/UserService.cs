using Api.Data;
using Api.Models;
using Api.ViewModels;
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
    
    public async Task<List<UserDetailsViewModel>> GetUsers()
    {
        var users = await _context.Users.ToListAsync();
        
        return users.Select(user => new UserDetailsViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Phone = user.Phone,
            IsAdmin = user.IsAdmin
        }).ToList();
    }

    public async Task<UserViewModel?> GetId(int id)
    { 
        var user = await _context.Users.FindAsync(id);
        
        if (user is null)
            return null;
        
        return new UserViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email
        };
    }
    
    public async Task<UserViewModel>? GetByUsername (string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        
        if (user is null)
            return null;
        
        return new UserViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email
        };
    }
    
    public async Task<UserViewModel>? GetByEmail (string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        
        if (user is null)
            return null;
        
        return new UserViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email
        };
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
        
        return new UserViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email
        };
    }
    
    public async Task<UserDetailsViewModel>? UpdateUser(int id, User request)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        user.Username = request.Username;
        user.Name = request.Name;
        user.Surname = request.Surname;
        user.Email = request.Email.ToLower();
        user.Phone = request.Phone;
        user.Password = request.Password;
        user.IsAdmin = request.IsAdmin;

        var password = request.Password;
        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
        
        user.Password = hash;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return new UserDetailsViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Phone = user.Phone,
            IsAdmin = user.IsAdmin
        };
    }

    public async Task<UserDetailsViewModel>? DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return null;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        
        return new UserDetailsViewModel
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Surname = user.Surname,
            Email = user.Email,
            Phone = user.Phone,
            IsAdmin = user.IsAdmin
        };
    }

    
}