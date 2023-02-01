using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;
using Api.ViewModels;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<CommentViewModel>>> GetComments()
        {
            return await _commentService.GetComments();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentViewModel>> GetComment(int id)
        {
            var result = await _commentService.GetId(id);
            return result == null ? NotFound("Comment not found") : Ok(result);
        }
        
        [HttpGet("article/{id}")]
        public async Task<ActionResult<List<CommentViewModel>>> GetByArticle(int id)
        {
            var result = await _commentService.GetByArticle(id);
            return result == null ? NotFound("Comments not found") : Ok(result);
        }
        
        [HttpGet("user/{username}")]
        [Authorize(Roles = "Admin, Provider, User")]
        public async Task<ActionResult<List<CommentViewModel>>> GetByUser(string username)
        {
            var result = await _commentService.GetByUser(username);
            return result == null ? NotFound("Comments not found") : Ok(result);
        }
        
        
        [HttpPost]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<Comment>> CreateComment(Comment comment)
        {
            var result = await _commentService.CreateComment(comment);
            return result == null ? Unauthorized("Comment already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<Comment>> UpdateComment(int id, Comment comment)
        {
            if (User.IsInRole("User"))
            {
                var commentToUpdate = await _commentService.GetId(id);
                if (commentToUpdate == null) return NotFound("Comment not found");
                if (commentToUpdate.UserId != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this comment");  
            } 
            
            var result = await _commentService.UpdateComment(id, comment);
            return result == null ? NotFound("Comment not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<Comment>> DeleteComment(int id)
        {
            if (User.IsInRole("User"))
            {
                var commentToDelete = await _commentService.GetId(id);
                if (commentToDelete == null) return NotFound("Comment not found");
                if (commentToDelete.UserId != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this comment");  
            }
            
            var result = await _commentService.DeleteComment(id);
            return result == null ? NotFound("Comment not found") : Ok(result);
        }
    }
}