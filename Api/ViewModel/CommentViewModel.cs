using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public int UserId { get; set; }
        public int ArticleId { get; set; }
        
        
        
        public CommentViewModel(Comment comment)
        {
            Id = comment.Id;
            Message = comment.Message;
            Rating = comment.Rating;
            UserId = comment.UserId;
            ArticleId = comment.ArticleId;
        }
    }
    
    
}
