using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class ArticleService : IArticleService
{
    
    private readonly DataContext _context;

    public ArticleService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<Article>> GetArticles()
    {
        var articles = await _context.Articles
            .Include(a => a.Provider)
            .Include(a => a.Category)
            .ToListAsync();
        return articles;
    }

    public async Task<Article?> GetId(int id)
    {
        var article = await _context.Articles
            .Include(a => a.Provider)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (article is null)
            return null;

        return article;
    }
    
    public async Task<List<Article>> CreateArticle(Article article)
    {
        article.Provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == article.ProviderId);
        article.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == article.CategoryId);
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        return await GetArticles();
    }
    
    public async Task<List<Article>?> UpdateArticle(int id, Article request)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article is null)
            return null;

        article.Name = request.Name;
        article.Description = request.Description;
        article.Image = request.Image;
        article.Year = request.Year;
        article.Price = request.Price;
        article.ProviderId = request.ProviderId;
        article.Provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == article.ProviderId);
        article.CategoryId = request.CategoryId;
        article.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == article.CategoryId);
        article.Stock = request.Stock;
        

        await _context.SaveChangesAsync();

        return await _context.Articles.ToListAsync();
    }

    public async Task<List<Article>?> DeleteArticle(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article is null)
            return null;

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();

        return await _context.Articles.ToListAsync();
    }

    
}