using MediatR;
using Domain.Abstractions;

namespace Application.Commands.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, SignInCommandResponse>
{
    private readonly IAuthService _authService;

    public SignInCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<SignInCommandResponse> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var signIn = await _authService.SignInAsync(request.Email, request.Password);
        return new SignInCommandResponse(signIn.BearerToken, signIn.RefreshToken, signIn.RefreshTokenValidTo);
    }
}
