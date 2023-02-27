using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface ICartService
{
    Task<List<CartViewModel>> GetCarts();
    Task<CartViewModel?> GetId(int id);
    Task<CartViewModel> CreateCart(Cart user);
    Task<CartViewModel>? UpdateCart(int id, Cart user);
    Task<CartViewModel>? DeleteCart(int id);
}