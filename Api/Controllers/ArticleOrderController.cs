using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleOrderController : ControllerBase
    {
        private readonly IArticleOrderService _articleOrderService;
        private readonly IProviderService _providerService;
        private readonly IUserService _userService;

        public ArticleOrderController(IArticleOrderService articleOrderService, IProviderService providerService, IUserService userService)
        {
            _articleOrderService = articleOrderService;
            _providerService = providerService;
            _userService = userService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<List<ArticleOrder>>> GetArticleOrders()
        {
            return await _articleOrderService.GetArticleOrders();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleOrder>> GetArticleOrder(int id)
        {
            var result = await _articleOrderService.GetId(id);
            return result == null ? NotFound("ArticleOrder not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<ArticleOrder>> CreateArticleOrder(Object articleOrder)
        {
            // var result = await _articleOrderService.CreateArticleOrder(articleOrder);
            // return result == null ? Unauthorized("ArticleOrder already exist") : Ok(result);
            
            var test = articleOrder;
            return Ok(test);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<ArticleOrder>> UpdateArticleOrder(int id, ArticleOrder articleOrder)
        {
            var result = await _articleOrderService.UpdateArticleOrder(id, articleOrder);
            return result == null ? NotFound("ArticleOrder not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArticleOrder>> DeleteArticleOrder(int id)
        {
            var result = await _articleOrderService.DeleteArticleOrder(id);
            return result == null ? NotFound("ArticleOrder not found") : Ok(result);
        }
    }
}