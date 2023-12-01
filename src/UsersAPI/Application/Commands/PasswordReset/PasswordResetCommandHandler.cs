using MediatR;
using Domain.Abstractions;

namespace Application.Commands.PasswordReset;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, Unit>
{
    private readonly IAuthService _authService;

    public PasswordResetCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Unit> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
    {
        await _authService.ResetPasswordAsync(request.ResetPasswordToken, request.PasswordResetDto);
        return Unit.Value;
    }
}
