namespace Application.Commands.RefreshToken;

public sealed record RefreshTokenCommandResponse(string bearerToken, string refreshToken, DateTime refreshTokenValidTo);
