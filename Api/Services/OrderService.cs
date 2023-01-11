using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class OrderService : IOrderService
{
    
    private readonly DataContext _context;

    public OrderService(DataContext context)
    {
        _context = context;
    }
    
    

    public async Task<List<Order>> GetOrders()
    {
        var orders = await _context.Orders.ToListAsync();
        return orders;
    }

    public async Task<Order?> GetId(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
            return null;

        return order;
    }
    
    public async Task<List<Order>> CreateOrder(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return await _context.Orders.ToListAsync();
    }
    
    public async Task<List<Order>?> UpdateOrder(int id, Order request)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
            return null;

        order.UserId = request.UserId;
        order.Date = request.Date;
        order.Status = request.Status;
        order.Serial = request.Serial;
        order.ArticleOrders = request.ArticleOrders;

        await _context.SaveChangesAsync();

        return await _context.Orders.ToListAsync();
    }

    public async Task<List<Order>?> DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
            return null;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return await _context.Orders.ToListAsync();
    }

    
}