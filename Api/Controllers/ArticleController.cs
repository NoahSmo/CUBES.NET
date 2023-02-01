﻿using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;
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
        public async Task<ActionResult<List<Article>>> GetArticles()
        {
            return await _articleService.GetArticles();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var result = await _articleService.GetId(id);
            return result == null ? NotFound("Article not found") : Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Article>> CreateArticle(Article article)
        {
            var result = await _articleService.CreateArticle(article);
            return result == null ? Unauthorized("Article already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Article>> UpdateArticle(int id, Article article)
        {
            if (User.IsInRole("Provider"))
            {
                var articleToUpdate = await _articleService.GetId(id);
                if (articleToUpdate == null) return NotFound("Article not found");
                if (articleToUpdate.ProviderId != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this article");
            }
            
            
            var result = await _articleService.UpdateArticle(id, article);
            return result == null ? NotFound("Article not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            if (User.IsInRole("Provider"))
            {
                var articleToDelete = await _articleService.GetId(id);
                if (articleToDelete == null) return NotFound("Article not found");
                if (articleToDelete.ProviderId != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this article");
            }
            
            var result = await _articleService.DeleteArticle(id);
            return result == null ? NotFound("Article not found") : Ok(result);
        }
    }
}
