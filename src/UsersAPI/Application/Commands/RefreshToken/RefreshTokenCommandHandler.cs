using MediatR;
using Domain.Abstractions;

namespace Application.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenCommandResponse>
{
    private readonly IAuthService _userService;

    public RefreshTokenCommandHandler(IAuthService userService)
    {
        _userService = userService;
    }

    public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var signIn = await _userService.RefreshTokenAsync(request.RefreshToken);

        return new RefreshTokenCommandResponse(signIn.BearerToken, signIn.RefreshToken, signIn.RefreshTokenValidTo);
    }
}
