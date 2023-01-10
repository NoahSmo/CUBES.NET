using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IUserService
{
    IEnumerable<UserDetails> GetUsers();
    UserDetails GetId(int id);
    IActionResult CreateUser(UserDetails user);
    IActionResult UpdateUser(User user);
    IActionResult DeleteUser(int id);
}