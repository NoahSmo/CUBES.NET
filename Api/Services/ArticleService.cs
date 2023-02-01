using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

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
            .Include(a => a.Domain)
            .Include(a => a.Category)
            .ToListAsync();
        return articles;
    }

    public async Task<Article?> GetId(int id)
    {
        var article = await _context.Articles
            .Include(a => a.Domain)
            .Include(a => a.Category)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (article is null)
            return null;

        return article;
    }
    
    public async Task<Article> CreateArticle(Article article)
    {
        article.Domain = await _context.Domains.FirstOrDefaultAsync(d => d.Id == article.DomainId);
        article.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == article.CategoryId);
        article.Provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == article.ProviderId);
        _context.Articles.Add(article);
        await _context.SaveChangesAsync();
        
        return article;
    }
    
    public async Task<Article>? UpdateArticle(int id, Article request)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article is null)
            return null;

        article.Name = request.Name;
        article.Description = request.Description;
        article.Year = request.Year;
        article.Price = request.Price;
        article.DomainId = request.DomainId;
        article.Domain = await _context.Domains.FirstOrDefaultAsync(d => d.Id == article.DomainId);
        article.CategoryId = request.CategoryId;
        article.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == article.CategoryId);
        article.ProviderId = request.ProviderId;
        article.Provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == article.ProviderId);
        article.Stock = request.Stock;
        
        _context.Articles.Update(article);
        await _context.SaveChangesAsync();

        return article;
    }
    
    public async Task<Article?> UpdateStock(Article article)
    {
        var articleToUpdate = await _context.Articles.FindAsync(article.Id);
        if (articleToUpdate is null)
            return null;

        articleToUpdate.Stock = article.Stock;
        await _context.SaveChangesAsync();

        return articleToUpdate;
    }

    public async Task<Article>? DeleteArticle(int id)
    {
        var article = await _context.Articles.FindAsync(id);
        if (article is null)
            return null;

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();

        return article;
    }

    
}