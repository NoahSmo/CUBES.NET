using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ArticleOrderService : IArticleOrderService
{
    
    private readonly DataContext _context;

    public ArticleOrderService(DataContext context)
    {
        _context = context;
    }


    public Task<List<ArticleOrder?>> GetArticleOrders()
    {
        var articleOrders = _context.ArticleOrder
            .Include(ao => ao.Article)
            .ToList();
        
        return Task.FromResult(articleOrders);
    }

    public Task<ArticleOrder?> GetId(int id)
    {
        var articleOrder = _context.ArticleOrder
            .Include(ao => ao.Article)
            .FirstOrDefault(ao => ao.Id == id);
        
        return Task.FromResult(articleOrder);
    }

    public Task<ArticleOrder> CreateArticleOrder(ArticleOrder articleOrder)
    {
        articleOrder.Id = _context.ArticleOrder.Max(a => a.Id) + 1;
        articleOrder.Article = _context.Articles.FirstOrDefault(a => a.Id == articleOrder.ArticleId);

        _context.ArticleOrder.Add(articleOrder);

        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return Task.FromResult(articleOrder);
    }

    public Task<ArticleOrder>? UpdateArticleOrder(int id, ArticleOrder articleOrder)
    {
        var articleOrderToUpdate = _context.ArticleOrder.FirstOrDefault(ao => ao.Id == id);
        if (articleOrderToUpdate is null)
            return null;
        
        articleOrderToUpdate.ArticleId = articleOrder.ArticleId;
        articleOrderToUpdate.Article = _context.Articles.FirstOrDefault(a => a.Id == articleOrder.ArticleId);
        if (articleOrderToUpdate.OrderId == null) articleOrderToUpdate.OrderId = articleOrder.OrderId;
        if (articleOrderToUpdate.ProviderOrderId == null) articleOrderToUpdate.ProviderOrderId = articleOrder.ProviderOrderId;
        articleOrderToUpdate.Quantity = articleOrder.Quantity;
        
        _context.ArticleOrder.Update(articleOrderToUpdate);
        
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return Task.FromResult(articleOrderToUpdate);
    }

    public Task<ArticleOrder>? DeleteArticleOrder(int id)
    {
        var articleOrderToDelete = _context.ArticleOrder.FirstOrDefault(ao => ao.Id == id);
        if (articleOrderToDelete is null)
            return null;
        
        _context.ArticleOrder.Remove(articleOrderToDelete);
        
        try
        {
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return Task.FromResult(articleOrderToDelete);
    }
}