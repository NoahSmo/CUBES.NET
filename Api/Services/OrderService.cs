using Api.Data;
using Api.Models;
using Api.ViewModels;
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
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.ArticleOrders)
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
        order.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == order.UserId);
        // order.User = new UserDetailsViewModel
        // {
        //     Id = user.Id,
        //     Username = user.Username,
        //     Name = user.Name,
        //     Surname = user.Surname,
        //     Email = user.Email,
        //     Phone = user.Phone,
        //     Address = user.Address,
        //     City = user.City,
        //     Country = user.Country,
        //     PostCode = user.PostCode
        // };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        
        return order;
    }
    
    public async Task<Order>? UpdateOrder(int id, Order request)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
            return null;

        order.UserId = request.UserId;
        
        order.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == order.UserId);
        // order.User = new UserDetailsViewModel
        // {
        //     Id = user.Id,
        //     Username = user.Username,
        //     Name = user.Name,
        //     Surname = user.Surname,
        //     Email = user.Email,
        //     Phone = user.Phone,
        //     Address = user.Address,
        //     City = user.City,
        //     Country = user.Country,
        //     PostCode = user.PostCode
        // };
        
        order.Date = request.Date;
        order.Status = request.Status;
        order.Serial = request.Serial;
        order.ArticleOrders = request.ArticleOrders;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<Order>? DeleteOrder(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order is null)
            return null;

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();

        return order;
    }

    
}