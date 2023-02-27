using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Services;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetCategories()
        {
            return await _categoryService.GetCategories();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var result = await _categoryService.GetId(id);
            return result == null ? NotFound("Category not found") : Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            var result = await _categoryService.CreateCategory(category);
            return result == null ? Unauthorized("Category already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Category>> UpdateCategory(int id, Category category)
        {
            var result = await _categoryService.UpdateCategory(id, category);
            return result == null ? NotFound("Category not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]        
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Category>> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return result == null ? NotFound("Category not found") : Ok(result);
        }
    }
}