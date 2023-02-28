using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data;
using Api.Models;
using Api.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class OrderService : IOrderService
{
    
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<Order>> GetOrders()
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.Address)
            .Include(o => o.Status)
            .Include(o => o.ArticleOrders)
            .ThenInclude(ao => ao.Article)
            .ToListAsync();
    }

    public async Task<Order?> GetId(int id)
    {
        var order = await _context.Orders
            .Include(o => o.User)
            .Include(o => o.ArticleOrders)
            .FirstOrDefaultAsync(o => o.Id == id);
        if (order is null)
            return null;

        return order;
    }
    
    public async Task<Order> CreateOrder(Order order)
    {
        order.Id = _context.Orders.Max(o => o.Id) + 1;
        order.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == order.UserId);
        order.Address = await _context.Addresses.FirstOrDefaultAsync(a => a.Id == order.AddressId);
        order.Status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == order.StatusId);
        order.Date = DateTime.Now.ToUniversalTime();
        
        var articleOrders = new List<ArticleOrder>();
        articleOrders = order.ArticleOrders;
        order.ArticleOrders = null;
        
        _context.Orders.Add(order);
        
        if (articleOrders != null)
        {
            foreach (var articleOrder in articleOrders)
            {
                articleOrder.Id = _context.ArticleOrder.Max(ao => ao.Id) + 1;
                articleOrder.Article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == articleOrder.ArticleId);
                articleOrder.OrderId = order.Id;
                
                _context.ArticleOrder.Add(articleOrder);
            } 
        }
        
        var orderArticles = new List<ArticleOrder>();
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return order;
    }
    
    public async Task<Order>? UpdateOrder(int id, Order request)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) return null;

        order.UserId = request.UserId;
        order.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == order.UserId);
        
        order.StatusId = request.StatusId;
        order.Status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == request.StatusId);

        order.Date = request.Date;
        order.ArticleOrders = request.ArticleOrders;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order>? DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null) return null;
        
        _context.Orders.Remove(order);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return null;
        }
        
        return order;
    }

    
}