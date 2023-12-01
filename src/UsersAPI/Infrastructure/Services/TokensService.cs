using Microsoft.IdentityModel.Tokens;
using Infrastructure.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static AuthProvider;

namespace Infrastructure.Services;

public class TokensService : ITokensService
{
    private readonly ISecureTokenService _secureTokenService;

    public TokensService(ISecureTokenService secureTokenService)
    {
        _secureTokenService = secureTokenService;
    }

    public string IssueBearerToken(List<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret));

        var token = new JwtSecurityToken(
            issuer: JwtIssuer,
            expires: DateTime.Now.AddMinutes(15),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
            audience: JwtIssuer
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string IssueSecureToken()
    {
        return _secureTokenService.GenerateToken();
    }
}
