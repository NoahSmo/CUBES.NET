using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IArticleService _articleService;
        private readonly IProviderOrderService _providerOrderService;

        public OrderController(IOrderService orderService, IArticleService articleService, IProviderOrderService providerOrderService)
        {
            _orderService = orderService;
            _articleService = articleService;
            _providerOrderService = providerOrderService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            return await _orderService.GetOrders();
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Provider, User")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (User.IsInRole("User"))
            {
                var order = await _orderService.GetId(id);
                if (order.UserId.ToString() != User.Identity?.Name)
                {
                    return Unauthorized("You are not authorized to access this order");
                }
            }

            var result = await _orderService.GetId(id);
            return result == null ? NotFound("Order not found") : Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var result = await _orderService.CreateOrder(order);
            if (result == null)
            {
                return Unauthorized("Order Id or Serial already exists");
            }

            foreach (var articleOrder in order.ArticleOrders)
            {
                var article = await _articleService.GetId(articleOrder.ArticleId);
                article.Stock -= articleOrder.Quantity;
                await _articleService.UpdateStock(article);

                if (article.Stock <= 0 && article.AutoRestock)
                {
                    await _providerOrderService.CreateProviderOrderFromOrder(article);
                }
            }

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            if (User.IsInRole("User"))
            {
                var orderToUpdate = await _orderService.GetId(id);
                if (orderToUpdate == null) return NotFound("Order not found");
                if (orderToUpdate.UserId.ToString() != User.Identity?.Name)
                    return Unauthorized("You are not the owner of this order");
            }

            var result = await _orderService.UpdateOrder(id, order);
            return result == null ? NotFound("Order not found") : Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            if (User.IsInRole("User"))
            {
                var orderToUpdate = await _orderService.GetId(id);
                if (orderToUpdate == null) return NotFound("Order not found");
                if (orderToUpdate.UserId.ToString() != User.Identity?.Name)
                    return Unauthorized("You are not the owner of this order");
            }
            
            var result = await _orderService.DeleteOrder(id);
            return result == null ? NotFound("Order not found") : Ok(result);
        }
    }
}
