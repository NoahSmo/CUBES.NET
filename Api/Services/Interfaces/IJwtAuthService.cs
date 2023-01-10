using System.Security.Claims;
using Api.Models;

namespace Api;

public interface IJwtAuthService
{
    UserDetails Auth (string email, string password);
    string GenerateToken (string secure, List<Claim> claims);
}