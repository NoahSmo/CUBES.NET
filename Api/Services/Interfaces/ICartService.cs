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
    Task<CartViewModel> CreateCart(Cart cart);
    
    Task<CartViewModel> AddArticleToCart(int id, CartItem cartItem);
    Task<CartViewModel> RemoveArticleFromCart(int id, CartItem cartItem);

    Task<CartViewModel>? UpdateCart(int id, Cart cart);
    Task<CartViewModel>? EmptyCart(int id);
    Task<CartViewModel>? DeleteCart(int id);
}