using MediatR;
using Domain.Abstractions;
using MassTransit;
using Common;

namespace Application.Commands.SignUp;

public class SignUpCommandHandler : IRequestHandler<SignUpCommand, SignUpCommandResponse>
{
    private readonly IAuthService _authService;
    private readonly IPublishEndpoint _publishEndpoint;

    public SignUpCommandHandler(IAuthService authService, IPublishEndpoint publishEndpoint)
    {
        _authService = authService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<SignUpCommandResponse> Handle(SignUpCommand request, CancellationToken cancellationToken)
    {
        var (bearerToken, refreshToken, refreshTokenValidTo, emailConfirmationToken) = await _authService.SignUpAsync(request.ToSignUpDTO());

        await _publishEndpoint.Publish(new UserRegisteredEvent(request.Email));

        return new SignUpCommandResponse(bearerToken, refreshToken, refreshTokenValidTo);
    }
}
