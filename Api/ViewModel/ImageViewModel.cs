using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ImageViewModel
    {
        public string Url { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
    }
}