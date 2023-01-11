using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Models;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly IJwtAuthService _jwtAuthService;
        private readonly IConfiguration _configuration;
        
        public LoginController(IJwtAuthService JwtAuthService, IConfiguration configuration)
        {
            _jwtAuthService = JwtAuthService;
            _configuration = configuration;
        }
        
        [HttpPost]
        public IActionResult Login([FromBody]UserLogin user)
        {
            var userAuth = _jwtAuthService.Auth(user.Email, user.Password);
            if (userAuth != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userAuth.Email),
                    new Claim(ClaimTypes.Role, userAuth.IsAdmin ? "Admin" : "User")
                };
               var token =  _jwtAuthService.GenerateToken(_configuration["Jwt:Key"],claims);
               return Ok(new JsonResult(token));
            }
            return Unauthorized();
        }
        
        
        // [HttpPost("DesktopLogin")]
        // public IActionResult LoginDesktop([FromBody]UserLogin user)
        // {
        //     var userAuth = _jwtAuthService.Auth(user.Email, user.Password);
        //     if (userAuth != null)
        //     {
        //         var claims = new List<Claim>
        //         {
        //             new Claim(ClaimTypes.Email, userAuth.Email),
        //             new Claim(ClaimTypes.Role, userAuth.IsAdmin ? "Admin" : "User")
        //         };
        //         var token =  _jwtAuthService.GenerateToken(_configuration["Jwt:Key"],claims);
        //         return Ok(new JsonResult(token));
        //     }
        //     return Unauthorized();
        // }
    }
}