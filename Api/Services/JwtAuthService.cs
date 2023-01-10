// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Api.Models;
// using Microsoft.IdentityModel.Tokens;
//
// namespace Api;
//
// public class JwtAuthService : IJwtAuthService
// {
//
//     private readonly List<UserDetails> _users = new List<UserDetails>
//     {
//         new UserDetails { Id = 1, Username = "user1", Email = "user1@gmail.com", Password = "password1" },
//         new UserDetails { Id = 2, Username = "2", Email = "user2@gmail.com", Password = "password2" },
//     };
//     
//     public UserDetails Auth(string email, string password)
//     {
//         return _users.Where(x => x.Email.ToUpper() == email.ToUpper() && x.Password.Equals(password)).FirstOrDefault();
//     }
//     
//     // Fonction qui va générer le token
//     public string GenerateToken(string secure, List<Claim> claims)
//     {
//         var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secure));
//         var tokenDesc = new SecurityTokenDescriptor
//         {
//             Subject = new ClaimsIdentity(claims),
//             Expires = DateTime.UtcNow.AddMinutes(10),
//             SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
//         };
//         
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var securityToken = tokenHandler.CreateToken(tokenDesc);
//         return tokenHandler.WriteToken(securityToken);
//     }
// }