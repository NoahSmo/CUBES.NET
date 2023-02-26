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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Role>>> GetRoles()
        {
            return await _roleService.GetRoles();
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Role>> GetRole(int id)
        {
            var result = await _roleService.GetId(id);
            return result == null ? NotFound("Role not found") : Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Role>> CreateRole(Role role)
        {
            var result = await _roleService.CreateRole(role);
            return result == null ? Unauthorized("Role already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Role>> UpdateRole(int id, Role role)
        {
            var result = await _roleService.UpdateRole(id, role);
            return result == null ? NotFound("Role not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Role>> DeleteRole(int id)
        {
            var result = await _roleService.DeleteRole(id);
            return result == null ? NotFound("Role not found") : Ok(result);
        }
    }
}