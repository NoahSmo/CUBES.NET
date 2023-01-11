using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api;

public class JwtAuthService : IJwtAuthService
{

    private readonly IConfiguration _config;
    private readonly DataContext _context;
    
    public JwtAuthService(IConfiguration config, DataContext context)
    {
        _config = config;
        _context = context;
    }
    
    public User Auth(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);


        return user;

        // TODO check hash

    }
    
    // Fonction qui va générer le token
    public string GenerateToken(string secure, List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secure));
        var tokenDesc = new SecurityTokenDescriptor
        {
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"],
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:Expiry"])),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDesc);
        return tokenHandler.WriteToken(securityToken);
    }
}