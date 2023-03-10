using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? User { get; set; }
        public virtual List<CartItemViewModel>? CartItems { get; set; }
        
        public CartViewModel(Cart cart)
        {
            Id = cart.Id;
            UserId = cart.UserId;
            User = cart.User.Email;
            if (cart.CartItems != null) CartItems = cart.CartItems.ConvertAll(item => new CartItemViewModel(item));
        }
    }
    
    public class CartItemViewModel
    {
        public int Quantity { get; set; }
        
        public int ArticleId { get; set; }
        public string? Article { get; set; }
        
        public CartItemViewModel(CartItem cartItem)
        {
            Quantity = cartItem.Quantity;
            ArticleId = cartItem.ArticleId;
            Article = cartItem.Article.Name;
        }
    }
}
