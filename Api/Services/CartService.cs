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
        cart.Id = _context.Carts.Max(c => c.Id) + 1;


        if (cart.UserId == null) return null;
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


    public async Task<CartViewModel>? AddArticleToCart(int id, CartItem cartItem)
    {
        var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(c => c.Article)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (cart is null || cart.CartItems is null) return null;
        if (cart.CartItems == null) cart.CartItems = new List<CartItem>();
        
        cart.User = await _context.Users.FindAsync(cart.UserId);
        cartItem.Article = await _context.Articles.FindAsync(cartItem.ArticleId);

        foreach (var item in cart.CartItems)
        {
            if (item.ArticleId == cartItem.ArticleId)
            {
                item.Quantity += cartItem.Quantity;
                _context.CartItem.Update(item);
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
        }

        try
        {
            cartItem.Id = _context.CartItem.Max(c => c.Id) + 1;
        }
        catch (Exception e)
        {
            cartItem.Id = 1;
        }
        
        cart.CartItems.Add(cartItem);
        
        _context.CartItem.Add(cartItem);
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

    public async Task<CartViewModel>? RemoveArticleFromCart(int id, CartItem cartItem)
    {
        var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(c => c.Article)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (cart is null || cart.CartItems is null) return null;
        if (cart.CartItems == null) cart.CartItems = new List<CartItem>();
        
        cart.User = await _context.Users.FindAsync(cart.UserId);
        cartItem.Article = await _context.Articles.FindAsync(cartItem.ArticleId);

        foreach (var item in cart.CartItems)
        {
            if (item.ArticleId == cartItem.ArticleId)
            {
                cart.CartItems.Remove(item);
                _context.CartItem.Remove(item);
                
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

        return null;
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

    public async Task<CartViewModel>? EmptyCart(int id)
    {
        var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(c => c.Article)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (cart is null) return null;
        
        cart.User = await _context.Users.FindAsync(cart.UserId);

        cart.CartItems = new List<CartItem>();

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