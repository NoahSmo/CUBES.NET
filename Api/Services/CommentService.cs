using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class CommentService : ICommentService
{
    
    private readonly DataContext _context;

    public CommentService(DataContext context)
    {
        _context = context;
    }


    public async Task<List<CommentViewModel?>> GetComments()
    {
        var comments = await _context.Comments
            .Include(c => c.User)
            .ToListAsync();

        var commentViewModels = comments.Select(c => new CommentViewModel(c)).ToList();

        return commentViewModels;
    }

    public async Task<CommentViewModel?> GetId(int id)
    {
        var comment = await _context.Comments
            .Include(c => c.User)
            .FirstOrDefaultAsync(c => c.Id == id);
        
        if (comment == null) return null;
        
        return new CommentViewModel(comment);
    }

    public async Task<List<CommentViewModel?>> GetByArticle(int id)
    {
        var comments = await _context.Comments
            .Include(c => c.User)
            .Where(c => c.ArticleId == id)
            .ToListAsync();
        
        var commentViewModels = comments.Select(c => new CommentViewModel(c)).ToList();
        
        return commentViewModels;
    }

    public async Task<List<CommentViewModel?>> GetByUser(string username)
    {
        var comments = await _context.Comments
            .Include(c => c.User)
            .Where(c => c.User.Username == username)
            .ToListAsync();
        
        var commentViewModels = comments.Select(c => new CommentViewModel(c)).ToList();
        
        return commentViewModels;
    }

    public Task<Comment> CreateComment(Comment comment)
    {
        comment.User = _context.Users.FirstOrDefault(u => u.Id == comment.UserId);
        comment.Article = _context.Articles.FirstOrDefault(a => a.Id == comment.ArticleId);
        
        _context.Comments.Add(comment);
        _context.SaveChanges();
        return Task.FromResult(comment);
    }

    public Task<Comment>? UpdateComment(int id, Comment comment)
    {
        var commentToUpdate = _context.Comments.FirstOrDefault(o => o.Id == id);
        if (commentToUpdate == null) return null;
        
        commentToUpdate.ArticleId = comment.ArticleId;
        commentToUpdate.Article = _context.Articles.FirstOrDefault(a => a.Id == comment.ArticleId);
        
        commentToUpdate.UserId = comment.UserId;
        commentToUpdate.User = _context.Users.FirstOrDefault(u => u.Id == comment.UserId);
        
        commentToUpdate.Rating = comment.Rating;
        commentToUpdate.Message = comment.Message;
        
        _context.Comments.Update(commentToUpdate);
        _context.SaveChanges();
        return Task.FromResult(commentToUpdate);
    }

    public Task<Comment>? DeleteComment(int id)
    {
        var comment = _context.Comments.FirstOrDefault(o => o.Id == id);
        if (comment == null) return null;
        
        _context.Comments.Remove(comment);
        _context.SaveChanges();
        return Task.FromResult(comment);
    }
}