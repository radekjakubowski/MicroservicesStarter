using MediatR;

namespace Application.Commands.SignIn;

public record SignInCommand(string Email, string Password) : IRequest<SignInCommandResponse>;
