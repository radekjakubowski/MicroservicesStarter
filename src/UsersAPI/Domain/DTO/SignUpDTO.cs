namespace Domain.DTO;

public record SignUpDTO(
    string Email,
    string Password,
    string ConfirmPassword
);
