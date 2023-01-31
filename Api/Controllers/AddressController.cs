using Microsoft.AspNetCore.Mvc;
using Api.Models;
using Api.Services;
using Api.ViewModels;
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
        public async Task<ActionResult<List<AddressViewModel>>> GetAddresses()
        {
            return await _addressService.GetAddresses();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressViewModel>> GetAddress(int id)
        {
            var result = await _addressService.GetId(id);
            return result == null ? NotFound("Address not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<AddressViewModel>> CreateAddress(Address address)
        {
            var result = await _addressService.CreateAddress(address);
            return result == null ? NotFound("Address not found") : Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<AddressViewModel>> UpdateAddress(int id, Address address)
        {
            if (User.IsInRole("User"))
            {
                var addressToUpdate = await _addressService.GetId(id);
                if (addressToUpdate == null) return NotFound("Address not found");
                if (addressToUpdate.UserId != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this address");
            }
            
            if (User.IsInRole("Admin"))
            {
                var addressToUpdate = await _addressService.GetId(id);
                if (addressToUpdate == null) return NotFound("Address not found");
                if (addressToUpdate.UserId != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this address");
            }
            
            var result = await _addressService.UpdateAddress(id, address);
            return result == null ? NotFound("Address not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<AddressViewModel>> DeleteAddress(int id)
        {
            var result = await _addressService.DeleteAddress(id);
            return result == null ? NotFound("Address not found") : Ok(result);
        }
    }
}
