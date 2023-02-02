using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class CartViewModel
    {
        public int UserId { get; set; }
        public string? User { get; set; }
        public virtual List<CartItemViewModel>? CartItems { get; set; }
        
        public CartViewModel(Cart cart)
        {
            UserId = cart.UserId;
            User = cart.User.Email;
            CartItems = cart.CartItems.ConvertAll(item => new CartItemViewModel(item));
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
