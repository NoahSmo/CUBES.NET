using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Services;

public interface IProviderOrderService
{
    Task<List<ProviderOrder?>> GetProviderOrders();
    Task<ProviderOrder?> GetId(int id);
    Task<ProviderOrder> CreateProviderOrder(ProviderOrder providerOrder);
    Task<ProviderOrder> CreateProviderOrderFromOrder(Article article);
    Task<ProviderOrder>? UpdateProviderOrder(int id, ProviderOrder providerOrder);
    Task<ProviderOrder>? DeleteProviderOrder(int id);
}