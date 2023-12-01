using Domain.DTO;

namespace Domain.Abstractions;

public interface IAuthService
{
    Task<SignInDTO> SignInAsync(string email, string password);
    Task<SignInDTO> SignUpAsync(SignUpDTO signUpDto);
    Task ConfirmEmailAsync(string emailConfirmationToken);
    Task<SignInDTO> RefreshTokenAsync(string refreshToken);
    Task<string> InitiatePasswordResetAsync(string userEmail);
    Task ResetPasswordAsync(string passwordResetToken, PasswordResetDTO passwordResetDto);
}
