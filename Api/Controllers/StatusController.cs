using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Status>>> GetStatus()
        {
            return await _statusService.GetStatuses();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            var result = await _statusService.GetId(id);
            return result == null ? NotFound("Status not found") : Ok(result);
        }
        
        [HttpPost]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Status>> CreateStatus(Status status)
        {
            var result = await _statusService.CreateStatus(status);
            return result == null ? Unauthorized("Status already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Status>> UpdateStatus(int id, Status status)
        {
            var result = await _statusService.UpdateStatus(id, status);
            return result == null ? NotFound("Status not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<Status>> DeleteStatus(int id)
        {
            var result = await _statusService.DeleteStatus(id);
            return result == null ? NotFound("Status not found") : Ok(result);
        }
    }
}