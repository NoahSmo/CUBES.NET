using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Services;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<CartViewModel>>> GetCarts()
        {
            return await _cartService.GetCarts();
        }
        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CartViewModel>> GetCart(int id)
        {
            var result = await _cartService.GetId(id);
            return result == null ? NotFound("Cart not found") : Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<CartViewModel>> CreateCart(Cart cart)
        {
            var result = await _cartService.CreateCart(cart);
            return result == null ? Unauthorized("Cart already exist") : Ok(result);
        }
        
        
        [HttpPost("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<CartViewModel>> AddArticleToCart(int id, CartItem cartItem)
        {
            var result = await _cartService.AddArticleToCart(id, cartItem);
            return result == null ? NotFound("Cart not found") : Ok(result);
        }
        
        
        // [HttpPut("{id}")]
        // [Authorize(Roles = "Admin")]
        // public async Task<ActionResult<CartViewModel>> UpdateCart(int id, Cart cart)
        // {
        //     var result = await _cartService.UpdateCart(id, cart);
        //     return result == null ? NotFound("Cart not found") : Ok(result);
        // }
        
        [HttpPut("{id}")]      
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CartViewModel>> RemoveArticleFromCart(int id, CartItem cartItem)
        {
            var result = await _cartService.RemoveArticleFromCart(id, cartItem);
            return result == null ? NotFound("Cart not found") : Ok(result);
        }
        
        
        
        [HttpDelete("{id}")]        
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CartViewModel>> EmptyCart(int id)
        {
            var result = await _cartService.EmptyCart(id);
            return result == null ? NotFound("Cart not found") : Ok(result);
        }
        
        // [HttpDelete("{id}")]        
        // [Authorize(Roles = "Admin")]
        // public async Task<ActionResult<CartViewModel>> DeleteCart(int id)
        // {
        //     var result = await _cartService.DeleteCart(id);
        //     return result == null ? NotFound("Cart not found") : Ok(result);
        // }
    }
}