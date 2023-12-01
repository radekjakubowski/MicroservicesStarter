namespace Infrastructure.Services.Abstractions;

public interface ISecureTokenService
{
    string GenerateToken();
}
