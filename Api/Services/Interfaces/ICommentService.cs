using Api.Models;
using Api.ViewModels;

namespace Api;

public interface ICommentService
{
    Task<List<CommentViewModel?>> GetComments();
    Task<CommentViewModel?> GetId(int id);
    Task<List<CommentViewModel?>> GetByArticle(int id);
    Task<List<CommentViewModel?>> GetByUser(string username);
    Task<Comment> CreateComment(Comment comment);
    Task<Comment>? UpdateComment(int id, Comment comment);
    Task<Comment>? DeleteComment(int id);
}