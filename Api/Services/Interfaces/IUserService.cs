using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IUserService
{
    Task<List<User>> GetUsers();
    Task<User?> GetId(int id);
    Task<List<User>> CreateUser(User user);
    Task<List<User>?> UpdateUser(int id, User user);
    Task<List<User>?> DeleteUser(int id);
}