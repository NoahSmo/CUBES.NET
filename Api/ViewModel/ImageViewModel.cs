using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ImageViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int? ArticleId { get; set; }
        
        public ImageViewModel(Image image)
        {
            Id = image.Id;
            Url = image.Url;
            ArticleId = image.ArticleId;
        }
    }
}