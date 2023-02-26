using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderOrderController : ControllerBase
    {
        private readonly IProviderOrderService _providerOrderService;
        private readonly IProviderService _providerService;

        public ProviderOrderController(IProviderOrderService providerOrderService, IProviderService providerService)
        {
            _providerOrderService = providerOrderService;
            _providerService = providerService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ProviderOrder>>> GetProviderOrders()
        {
            return await _providerOrderService.GetProviderOrders();
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, ProviderOrder")]
        public async Task<ActionResult<ProviderOrder>> GetProviderOrder(int id)
        {
            if (User.IsInRole("Provider"))
            {
                var provider = await _providerService.GetId(id);
                if (provider.Name != User.Identity?.Name) return Unauthorized("You are not the owner of this providerOrder");
            }
            
            var result = await _providerOrderService.GetId(id);
            return result == null ? NotFound("ProviderOrder not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<ProviderOrder>> CreateProviderOrder(ProviderOrder providerOrder)
        {
            var result = await _providerOrderService.CreateProviderOrder(providerOrder);
            return result == null ? Unauthorized("ProviderOrder already exist") : Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProviderOrder>> UpdateProviderOrder(int id, ProviderOrder providerOrder)
        {
            var result = await _providerOrderService.UpdateProviderOrder(id, providerOrder);
            return result == null ? NotFound("ProviderOrder not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProviderOrder>> DeleteProviderOrder(int id)
        {
            var result = await _providerOrderService.DeleteProviderOrder(id);
            return result == null ? NotFound("ProviderOrder not found") : Ok(result);
        }
    }
}