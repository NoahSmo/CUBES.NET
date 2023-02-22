using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DomainController : ControllerBase
    {
        private readonly IDomainService _domainService;

        public DomainController(IDomainService domainService)
        {
            _domainService = domainService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Domain>>> GetDomains()
        {
            return await _domainService.GetDomains();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain>> GetDomain(int id)
        {
            var result = await _domainService.GetId(id);
            return result == null ? NotFound("Domain not found") : Ok(result);
        }
        
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Domain>> GetDomain(string name)
        {
            var result = await _domainService.GetByName(name);
            return result == null ? NotFound("Domain not found") : Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Domain>> CreateDomain(Domain domain)
        {
            var result = await _domainService.CreateDomain(domain);
            return result == null ? Unauthorized("Domain already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Domain>> UpdateDomain(int id, Domain domain)
        {
            var result = await _domainService.UpdateDomain(id, domain);
            return result == null ? NotFound("Domain not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]        
        [Authorize(Roles = "Admin, Provider")]
        public async Task<ActionResult<Domain>> DeleteDomain(int id)
        {
            //Todo DOMAIN DELETE
            
            var result = await _domainService.DeleteDomain(id);
            return result == null ? NotFound("Domain not found") : Ok(result);
        }
    }
}