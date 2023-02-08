using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IImageService
{
    Task<List<Image?>> GetImages();
    Task<Image?> GetId(int id);
    Task<Image> CreateImage(Image image);
    Task<Image>? UpdateImage(int id, Image image);
    Task<Image>? DeleteImage(int id);
}