using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Provider>>> GetProviders()
        {
            return await _providerService.GetProviders();
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Provider>> GetProvider(int id)
        {
            if (User.IsInRole("Provider"))
            {
                var provider = await _providerService.GetId(id);
                if (provider.Name != User.Identity?.Name) return Unauthorized("You are not the owner of this provider");
            }
            
            var result = await _providerService.GetId(id);
            return result == null ? NotFound("Provider not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<Provider>> CreateProvider(Provider provider)
        {
            var result = await _providerService.CreateProvider(provider);
            return result == null ? Unauthorized("Provider already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Provider>> UpdateProvider(int id, Provider provider)
        {
            var result = await _providerService.UpdateProvider(id, provider);
            return result == null ? NotFound("Provider not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Provider>> DeleteProvider(int id)
        {
            var result = await _providerService.DeleteProvider(id);
            return result == null ? NotFound("Provider not found") : Ok(result);
        }
    }
}