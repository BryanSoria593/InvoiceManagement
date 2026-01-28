using InvoiceManagement.Application.Auth.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InvoiceManagement.Infrastructure.Auth;

public class JwtTokenGenerator : IJwtTokenGenerator
{

    public JwtTokenGenerator()
    {
    }

    public string GenerateToken(int userId, string username, string? email, IEnumerable<KeyValuePair<string, string>>? extraClaims = null)
    {
        var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET")
            ?? throw new InvalidOperationException("JWT_SECRET no está configurado");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(2);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Email, email ?? "")
        };
        if (extraClaims != null)
        {
            foreach (var pair in extraClaims)
            {
                claims.Add(new Claim(pair.Key, pair.Value));
            }
        }

        var token = new JwtSecurityToken(
            issuer: "InvoiceManagement.Api",
            audience: "InvoiceManagement.Api",
            claims: claims,
            expires: expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
