using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<ArticleViewModel>>> GetArticles()
        {
            var result = await _articleService.GetArticles();
            return Ok(result);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var result = await _articleService.GetId(id);
            return result == null ? NotFound("Article not found") : Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<ArticleViewModel>> CreateArticle(Article article)
        {
            var result = await _articleService.CreateArticle(article);
            return result == null ? Unauthorized("Article already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<ArticleViewModel>> UpdateArticle(int id, Article article)
        {
            var result = await _articleService.UpdateArticle(id, article);
            return result == null ? NotFound("Article not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<ArticleViewModel>> DeleteArticle(int id)
        {
            var result = await _articleService.DeleteArticle(id);
            return result == null ? NotFound("Article not found") : Ok(result);
        }
    }
}
