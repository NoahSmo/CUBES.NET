using System;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

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
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _userService.GetUsers();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var result = await _userService.GetId(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var result = await _userService.CreateUser(user);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(int id, User user)
        {
            var result = await _userService.UpdateUser(id, user);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        
        



        // [HttpPost("login")]
        // public IActionResult Login([FromBody] User userData)
        // {
        //     var password = userData.Password;
        //     var saltedPassword = password + salt;
        //
        //     var hashedPasswordFromDb = "";
        //
        //
        //     using (var conn = new NpgsqlConnection(connectionString))
        //     {
        //         conn.Open();
        //         using (var cmd = new NpgsqlCommand("SELECT password FROM public.\"User\" WHERE email = @email", conn))
        //         {
        //             cmd.Parameters.AddWithValue("username", userData.Email);
        //             using (var reader = cmd.ExecuteReader())
        //             {
        //                 while (reader.Read())
        //                 {
        //                     hashedPasswordFromDb = reader.GetString(0);
        //                 }
        //             }
        //         }
        //     }
        //
        //     if (BCrypt.Net.BCrypt.Verify(saltedPassword, hashedPasswordFromDb))
        //     {
        //         return Ok("Logged in");
        //     }
        //     else
        //     {
        //         return BadRequest("Wrong password");
        //     }
        // }

        
    }
}