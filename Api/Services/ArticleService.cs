using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.ViewModels;
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
    
    

    public async Task<List<ArticleViewModel>> GetArticles()
    {
        var articles = await _context.Articles
            .Where(a => a.isDeactivated == false)
            .Include(a => a.Provider)
            .Include(a => a.Category)
            .Include(a => a.Images)
            .ToListAsync();
        
        return articles.Select(a => new ArticleViewModel(a)).ToList();
    }

    public async Task<Article?> GetId(int id)
    {
        var article = await _context.Articles
            .Include(a => a.Provider)
            .Include(a => a.Category)
            .Include(a => a.Images)
            .FirstOrDefaultAsync(a => a.Id == id);
        
        return article;
    }
    
    public async Task<ArticleViewModel> CreateArticle(Article article)
    {
        if (article.CategoryId != null) article.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == article.CategoryId);
        
        article.Id = _context.Articles.Max(x => x.Id) + 1;  
        
        _context.Articles.Add(article);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return new ArticleViewModel(article);
    }
    
    public async Task<ArticleViewModel>? UpdateArticle(int id, Article request)
    {
        var article = await GetId(id);
        if (article is null) return null;

        article.Name = request.Name;
        article.Description = request.Description;
        article.Year = request.Year;
        article.Alcohol = request.Alcohol;
        article.Price = request.Price;
        article.AutoRestock = request.AutoRestock;


        article.ProviderId = request.ProviderId;
        article.Provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == article.ProviderId);

        article.CategoryId = request.CategoryId;
        article.Category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == article.CategoryId);
        
        
        article.Stock = request.Stock;
        
        _context.Articles.Update(article);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }

        return new ArticleViewModel(article);
    }
    
    public async Task<Article?> UpdateStock(Article article)
    {
        var articleToUpdate = await _context.Articles.FindAsync(article.Id);
        if (articleToUpdate is null)
            return null;

        articleToUpdate.Stock = article.Stock;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }

        
        return articleToUpdate;
    }

    public async Task<ArticleViewModel>? DeleteArticle(int id)
    {
        var article = await GetId(id);
        if (article is null) return null;

        article.isDeactivated = true;

        _context.Articles.Update(article);
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }

        return new ArticleViewModel(article);
    }

    
}