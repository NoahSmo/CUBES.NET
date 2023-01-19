using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Microsoft.AspNetCore.Authorization;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Address>>> GetAddresss()
        {
            return await _addressService.GetAddresses();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var result = await _addressService.GetId(id);
            return result == null ? NotFound("Address not found") : Ok(result);
        }
        
        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Address>> CreateAddress(Address address)
        {
            var result = await _addressService.CreateAddress(address);
            return result == null ? NotFound("Address not found") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Address>> UpdateAddress(int id, Address address)
        {
            var result = await _addressService.UpdateAddress(id, address);
            return result == null ? NotFound("Address not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var result = await _addressService.DeleteAddress(id);
            return result == null ? NotFound("Address not found") : Ok(result);
        }
    }
}
