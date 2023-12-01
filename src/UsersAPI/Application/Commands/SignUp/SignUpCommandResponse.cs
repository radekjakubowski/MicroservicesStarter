namespace Application.Commands.SignUp;

public record SignUpCommandResponse(string BearerToken, string RefreshToken, DateTime RefreshTokenValidTo);
