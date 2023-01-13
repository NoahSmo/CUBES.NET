using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IUserService
{
    Task<List<UserDetailsViewModel>> GetUsers();
    Task<UserViewModel?> GetId(int id);
    Task<UserViewModel?> GetByUsername(string username);
    Task<UserViewModel?> GetByEmail(string email);
    Task<UserViewModel> CreateUser(User user);
    Task<UserDetailsViewModel>? UpdateUser(int id, User user);
    Task<UserDetailsViewModel>? DeleteUser(int id);
}