﻿using Microsoft.AspNetCore.Mvc;
using Api.Models;
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
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Article>> CreateArticle(Article article)
        {
            var result = await _articleService.CreateArticle(article);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Article>> UpdateArticle(int id, Article article)
        {
            var result = await _articleService.UpdateArticle(id, article);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Article>> DeleteArticle(int id)
        {
            var result = await _articleService.DeleteArticle(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
