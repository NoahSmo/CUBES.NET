using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class ImageViewModel
    {
        public string Url { get; set; }
        public int? ArticleId { get; set; }
        
        public ImageViewModel(Image image)
        {
            Url = image.Url;
            ArticleId = image.ArticleId;
        }
    }
}