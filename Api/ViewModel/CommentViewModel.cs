using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class CommentViewModel
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        
        
        
        public CommentViewModel(Comment comment)
        {
            Message = comment.Message;
            UserId = comment.UserId;
        }
    }
    
    
}
