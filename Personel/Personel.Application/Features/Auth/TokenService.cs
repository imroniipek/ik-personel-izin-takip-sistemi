using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Personel.Personel.Application.Features.Auth;

public class TokenService(IConfiguration configuration)
{
    public string CreateToken(int? personelId, string? email, string role, int? departmentId)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, role)
        };

        if (personelId is not null)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, personelId.Value.ToString()));
        }

        if (!string.IsNullOrWhiteSpace(email))
        {
            claims.Add(new Claim(ClaimTypes.Email, email));
        }

        if (departmentId is not null)
        {
            claims.Add(new Claim("departmentId", departmentId.Value.ToString()));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));// JWT: Key normalde bir string.Ama güvenlik sistemi byte ister.

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);  // SecurityAlgorithms.HmacSha256:  Tokenın sahte olup olmadığını kontrol eden matematiksel yöntem.

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}