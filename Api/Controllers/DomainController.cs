using Microsoft.AspNetCore.Mvc;
using Api.Models;
using System.Security.Claims;
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
            _domainService = _domainService;
        }
        
        
    }
}