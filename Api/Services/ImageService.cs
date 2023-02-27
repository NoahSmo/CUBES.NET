using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ImageService : IImageService
{
    private readonly DataContext _context;

    public ImageService(DataContext context)
    {
        _context = context;
    }


    public async Task<List<Image?>> GetImages()
    {
        return await _context.Images
            .Include(i => i.Article)
            .ToListAsync();
    }

    public async Task<Image?> GetId(int id)
    {
        return await _context.Images
            .Include(i => i.Article)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Image> CreateImage(Image image)
    {
        image.Article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == image.ArticleId);
        _context.Images.Add(image);
        await _context.SaveChangesAsync();
        return image;
    }

    public async Task<Image>? UpdateImage(int id, Image image)
    {
        var imageToUpdate = await _context.Images.FindAsync(id);
        if (imageToUpdate == null)
        {
            return null;
        }

        imageToUpdate.Url = image.Url;
        imageToUpdate.ArticleId = image.ArticleId;
        imageToUpdate.Article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == image.ArticleId);

        _context.Images.Update(imageToUpdate);
        await _context.SaveChangesAsync();
        return imageToUpdate;
    }

    public async Task<Image>? DeleteImage(int id)
    {
        var imageToDelete = await _context.Images.FindAsync(id);
        if (imageToDelete == null)
        {
            return null;
        }

        _context.Images.Remove(imageToDelete);
        await _context.SaveChangesAsync();
        return imageToDelete;
    }
}