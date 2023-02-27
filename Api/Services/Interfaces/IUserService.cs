using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IUserService
{
    Task<List<UserDetailsViewModel>> GetUsers();
    Task<List<User>> GetUsersWPF();
    Task<UserViewModel?> GetId(int id);
    Task<UserViewModel?> GetByEmail(string email);
    Task<UserViewModel> CreateUser(User user);
    Task<UserDetailsViewModel>? UpdateUser(int id, User user);
    Task<UserDetailsViewModel>? DeleteUser(int id);
}