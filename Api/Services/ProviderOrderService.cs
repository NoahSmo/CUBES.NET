using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class ProviderOrderService : IProviderOrderService
{
    
    private readonly DataContext _context;

    public ProviderOrderService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<ProviderOrder?>> GetProviderOrders()
    {
        return await _context.ProviderOrders
            .ToListAsync();
    }

    public async Task<ProviderOrder?> GetId(int id)
    {
        var providerOrders = await _context.ProviderOrders.FindAsync(id);
        if (providerOrders is null)
            return null;

        return providerOrders;
    }
    
    public async Task<ProviderOrder> CreateProviderOrder(ProviderOrder providerOrders)
    {
        _context.ProviderOrders.Add(providerOrders);
        await _context.SaveChangesAsync();
        return providerOrders;
    }
    
    public async Task<ProviderOrder> CreateProviderOrderFromOrder(Article article)
    {
        var providerOrders = new ProviderOrder();

        providerOrders.Id = _context.ProviderOrders.Max(p => p.Id) + 1;
        
        providerOrders.Date = DateTime.Today.ToUniversalTime();

        providerOrders.StatusId = 1;
        providerOrders.Status = await _context.Statuses.FirstOrDefaultAsync(s => s.Id == providerOrders.StatusId);

        providerOrders.ProviderId = article.ProviderId;
        providerOrders.Provider = await _context.Providers.FirstOrDefaultAsync(p => p.Id == article.ProviderId);

        var articleOrder = new ArticleOrder();
        articleOrder.ArticleId = article.Id;
        articleOrder.Quantity = 50;
        articleOrder.ProviderOrderId = providerOrders.Id;

        var articleOrders = new List<ArticleOrder>();
        articleOrders.Add(articleOrder);

        providerOrders.ArticleOrders = articleOrders;


        _context.ProviderOrders.Add(providerOrders);
        await _context.SaveChangesAsync();
        
        return providerOrders;
    }
    
    public async Task<ProviderOrder>? UpdateProviderOrder(int id, ProviderOrder providerOrders)
    {
        var providerOrdersToUpdate = await _context.ProviderOrders.FindAsync(id);
        if (providerOrdersToUpdate is null)
            return null;

        providerOrdersToUpdate.Date = providerOrders.Date;
        providerOrdersToUpdate.ProviderId = providerOrders.ProviderId;
        providerOrdersToUpdate.Provider = await _context.Providers.FindAsync(providerOrders.ProviderId);
        providerOrdersToUpdate.StatusId = providerOrders.StatusId;
        providerOrdersToUpdate.Status = await _context.Statuses.FindAsync(providerOrders.StatusId);
        
        _context.ProviderOrders.Update(providerOrdersToUpdate);
        await _context.SaveChangesAsync();

        return providerOrdersToUpdate;
    }

    public async Task<ProviderOrder>? DeleteProviderOrder(int id)
    {
        var providerOrder = await _context.ProviderOrders.FindAsync(id);
        if (providerOrder is null)
            return null;

        _context.ProviderOrders.Remove(providerOrder);
        await _context.SaveChangesAsync();

        return providerOrder;
    }

    
}