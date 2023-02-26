using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class CartService : ICartService
{
    
    private readonly DataContext _context;

    public CartService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<CartViewModel>> GetCarts()
    {
        var carts = await _context.Carts
            .Include(cart => cart.User)
            .Include(cart => cart.CartItems)
            .ThenInclude(cartItem => cartItem.Article)
            .ToListAsync();

        return carts.Select(cart => new CartViewModel(cart)).ToList();
    }

    public async Task<CartViewModel?> GetId(int id)
    {
        var cart = await _context.Carts
            .Include(cart => cart.User)
            .Include(cart => cart.CartItems)
            .ThenInclude(cartItem => cartItem.Article)
            .FirstOrDefaultAsync(cart => cart.Id == id);
        if (cart is null)
            return null;

        return new CartViewModel(cart);
    }
    
    public async Task<CartViewModel> CreateCart(Cart cart)
    {
        cart.User = await _context.Users.FindAsync(cart.UserId);
        
        cart.CartItems.ForEach(cartItem => cartItem.Cart = _context.Carts.Find(cartItem.CartId));
        
        cart.CartItems.ForEach(cartItem => cartItem.Article = _context.Articles.Find(cartItem.ArticleId));
        
        _context.Carts.Add(cart);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return new CartViewModel(cart);
    }
    
    public async Task<CartViewModel>? UpdateCart(int id, Cart request)
    {
        var cart = await _context.Carts.FindAsync(id);
        if (cart is null) return null;
        
        cart.User = await _context.Users.FindAsync(request.UserId);
        cart.CartItems = request.CartItems;
        
        cart.CartItems.ForEach(cartItem => cartItem.Cart = _context.Carts.Find(cartItem.CartId));
        cart.CartItems.ForEach(cartItem => cartItem.Article = _context.Articles.Find(cartItem.ArticleId));
        
        _context.Carts.Update(cart);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }

        return new CartViewModel(cart);
    }

    public async Task<CartViewModel>? DeleteCart(int id)
    {
        var cart = await _context.Carts.FindAsync(id);
        if (cart is null)
            return null;

        _context.Carts.Remove(cart);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }

        return new CartViewModel(cart);
    }

    
}