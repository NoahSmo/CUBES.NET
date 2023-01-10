using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public class UserService : IUserService
{
    
    private readonly DataContext _context;

    public UserService(DataContext context)
    {
        _context = context;
    }
    
    public IEnumerable<UserDetails> GetUsers()
    {
        var users = _context.Users.ToList();
    }

    public UserDetails GetId(int id)
    {
        var userDetail = UserDetails.FirstOrDefault(x => x.Id == id);
    }

    public IActionResult CreateUser(UserDetails user)
    {
        UserDetails.Add(user);
        return new OkResult();
    }

    public IActionResult UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public IActionResult DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}