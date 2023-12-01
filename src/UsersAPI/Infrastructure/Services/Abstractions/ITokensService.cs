using System.Security.Claims;

namespace Infrastructure.Services.Abstractions;

public interface ITokensService
{
    string IssueBearerToken(List<Claim> authClaims);
    string IssueSecureToken();
}
