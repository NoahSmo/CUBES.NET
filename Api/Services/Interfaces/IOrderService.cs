using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IOrderService
{
    Task<List<Order>> GetOrders();
    Task<Order?> GetId(int id);
    Task<List<Order>> CreateOrder(Order user);
    Task<List<Order>?> UpdateOrder(int id, Order user);
    Task<List<Order>?> DeleteOrder(int id);
}