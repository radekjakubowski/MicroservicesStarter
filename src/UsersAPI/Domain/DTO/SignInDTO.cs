namespace Domain.DTO;

public record SignInDTO(string BearerToken, string RefreshToken, DateTime RefreshTokenValidTo, string EmailConfirmationToken = null);
