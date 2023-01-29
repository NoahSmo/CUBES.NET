using Microsoft.AspNetCore.Mvc;
using Api.Models;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DomainAddressController : ControllerBase
    {
        private readonly IDomainAddressService _domainAddressService;

        public DomainAddressController(IDomainAddressService domainAddressService)
        {
            _domainAddressService = domainAddressService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<DomainAddress>>> GetDomains()
        {
            return await _domainAddressService.GetDomainAddresses();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<DomainAddress>> GetDomain(int id)
        {
            var result = await _domainAddressService.GetId(id);
            return result == null ? NotFound("Domain not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<DomainAddress>> CreateDomain(DomainAddress domainAddress)
        {
            var result = await _domainAddressService.CreateDomainAddress(domainAddress);
            return result == null ? Unauthorized("Domain already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<DomainAddress>> UpdateDomain(int id, DomainAddress domainAddress)
        {
            var result = await _domainAddressService.UpdateDomainAddress(id, domainAddress);
            return result == null ? NotFound("Domain not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<DomainAddress>> DeleteDomain(int id)
        {
            var result = await _domainAddressService.DeleteDomainAddress(id);
            return result == null ? NotFound("Domain not found") : Ok(result);
        }
    }
}