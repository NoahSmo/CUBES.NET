using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IOrderService
{
    Task<List<Order>> GetOrders();
    Task<Order?> GetId(int id);
    Task<Order> CreateOrder(Order user);
    Task<Order>? UpdateOrder(int id, Order user);
    Task<Order>? DeleteOrder(int id);
}