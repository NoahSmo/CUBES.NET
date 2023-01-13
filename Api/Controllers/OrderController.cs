using Microsoft.AspNetCore.Mvc;
using Api.Models;
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
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            return await _orderService.GetOrders();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var result = await _orderService.GetId(id);
            if (result == null)
            {
                return NotFound("Order not found");
            }
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize (Roles = "Admin")]
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
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Order>> UpdateOrder(int id, Order order)
        {
            var result = await _orderService.UpdateOrder(id, order);
            if (result == null)
            {
                return NotFound("Order not found");
            }
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            if (result == null)
            {
                return NotFound("Order not found");
            }
            return Ok(result);
        }
    }
}
