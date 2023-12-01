using MediatR;
using Domain.Abstractions;

namespace Application.Commands.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpCommandResponse>
{
    private readonly IAuthService _authService;

    public SignUpCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<SignUpCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var (bearerToken, refreshToken, refreshTokenValidTo, emailConfirmationToken) = await _authService.SignUpAsync(request.ToSignUpDTO());

        // user signed up event

        return new SignUpCommandResponse(bearerToken, refreshToken, refreshTokenValidTo);
    }
}
