using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Api.ViewModels;


namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<UserDetailsViewModel>>> GetUsers()
        {
            return await _userService.GetUsers();
        }
        
        [HttpGet("wpf")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<User>>> GetUsersWPF()
        {
            return await _userService.GetUsersWPF();
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Provider, User")]
        public async Task<ActionResult<UserViewModel>> GetUser(int id)
        {
            if (User.IsInRole("User"))
            {
                var user = await _userService.GetId(id);
                if (user == null) return NotFound("User not found");
                if (user.Id != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this user");
            }
            
            var result = await _userService.GetId(id);
            return result == null ? NotFound("User not found") : Ok(result);
        }
        
        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin, Provider, User")]
        public async Task<ActionResult<UserViewModel>> GetUserByMail(string email)
        {
            if (User.IsInRole("User"))
            {
                var user = await _userService.GetByEmail(email);
                if (user == null) return NotFound("User not found");
                if (user.Id != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this user");
            }
            
            var result = await _userService.GetByEmail(email);
            return result == null ? NotFound("User not found") : Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var result = await _userService.CreateUser(user);
            return result == null ? Unauthorized("User already exist") : Ok(result);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            if (User.IsInRole("User"))
            {
                var userData = await _userService.GetId(id);
                if (userData == null) return NotFound("User not found");
                if (userData.Id != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this user");
            }
            
            var result = await _userService.UpdateUser(id, user);
            return result == null ? NotFound("User not found") : Ok(result);
        }
        
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            if (User.IsInRole("User"))
            {
                var userData = await _userService.GetId(id);
                if (userData == null) return NotFound("User not found");
                if (userData.Id != int.Parse(User.Identity?.Name)) return Unauthorized("You are not the owner of this user");
            }
            
            var result = await _userService.DeleteUser(id);
            return result == null ? NotFound("User not found") : Ok(result);
        }
    }
}