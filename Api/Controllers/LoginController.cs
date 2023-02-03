using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Api.Models;
using Api.ViewModels;

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
            var userAuth = _jwtAuthService.Auth(user.Email.ToLower(), user.Password);
            if (userAuth != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, userAuth.Email),
                    new Claim(ClaimTypes.NameIdentifier, userAuth.Id.ToString()),
                    new Claim(ClaimTypes.Role, userAuth.Role?.Name ?? "User"),
                    new Claim(CustomClaimTypes.Permission, userAuth.Role?.Permissions?.Select(x => x.Name).Aggregate((x, y) => x + "," + y) ?? "User")
                };
               var token =  _jwtAuthService.GenerateToken(_configuration["Jwt:Key"],claims);
               return Ok(new JsonResult(token));
            }
            return Unauthorized("Invalid credentials");
        }
        
        
        [HttpPost("DesktopLogin")]
        public IActionResult LoginDesktop([FromBody]UserLogin user)
        {
            var userAuth = _jwtAuthService.Auth(user.Email, user.Password);
            if (userAuth != null)
            { 
                var userDetails = new UserDetailsViewModel(userAuth);

                if (userAuth.Role.Name == "Provider" || userAuth.Role.Name == "User")
                {
                    return Unauthorized("You are not allowed to access this application");
                }
                
                return userAuth.Role == null ? Unauthorized("Invalid credentials") : Ok(userDetails);
            }
            return Unauthorized("Invalid credentials");
        }
    }
}