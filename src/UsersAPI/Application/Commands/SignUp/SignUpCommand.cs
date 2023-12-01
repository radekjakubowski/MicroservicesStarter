using MediatR;
using Domain.DTO;

namespace Application.Commands.SignUp;

public sealed record SignUpCommand(
    string Email, 
    string Password, 
    string ConfirmPassword
) : IRequest<SignUpCommandResponse>
{
    public SignUpDTO ToSignUpDTO()
    {
        return new SignUpDTO(Email, Password, ConfirmPassword);
    }
};
