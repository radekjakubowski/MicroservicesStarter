using Infrastructure.Services.Abstractions;
using System.Security.Cryptography;

namespace OddajGlos.Users.Infrastructure.Services.Infrastructure;

public class SecureTokenService : ISecureTokenService
{
    public string GenerateToken()
    {
        byte[] randomBytes = new byte[32]; // 256 bits / 8 bits per byte = 32 bytes
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        // Convert bytes to a secure token
        string token = BitConverter.ToString(randomBytes).Replace("-", "").ToLower();

        return token;
    }
}
