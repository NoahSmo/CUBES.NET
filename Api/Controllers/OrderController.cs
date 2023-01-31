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

        public OrderController(IOrderService orderService, IArticleService articleService)
        {
            _orderService = orderService;
            _articleService = articleService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            return await _orderService.GetOrders();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var result = await _orderService.GetId(id);
            return result == null ? NotFound("Order not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var result = await _orderService.CreateOrder(order);
            if (result == null)
            {
                return Unauthorized("Order Id or Serial already exists");
            }
                
            order.ArticleOrders.ForEach(async x =>
            {
                var article = await _articleService.GetId(x.ArticleId);
                article.Stock -= x.Quantity;
                await _articleService.UpdateStock(article);
            });
            
            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            var result = await _orderService.UpdateOrder(id, order);
            return result == null ? NotFound("Order not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            return result == null ? NotFound("Order not found") : Ok(result);
        }
    }
}
