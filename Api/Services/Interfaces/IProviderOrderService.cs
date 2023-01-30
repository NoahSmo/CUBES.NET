using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api;

public interface IProviderOrderService
{
    Task<List<ProviderOrder?>> GetProviderOrders();
    Task<ProviderOrder?> GetId(int id);
    Task<ProviderOrder> CreateProviderOrder(ProviderOrder providerOrder);
    Task<ProviderOrder>? UpdateProviderOrder(int id, ProviderOrder providerOrder);
    Task<ProviderOrder>? DeleteProviderOrder(int id);
}