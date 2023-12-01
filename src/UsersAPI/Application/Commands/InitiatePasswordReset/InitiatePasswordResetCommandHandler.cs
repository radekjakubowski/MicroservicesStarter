using MediatR;
using Domain.Abstractions;

namespace Application.Commands.PasswordReset;

public class InitiatePasswordResetCommandHandler : IRequestHandler<InitiatePasswordResetCommand, Unit>
{
    private readonly IAuthService _authService;

    public InitiatePasswordResetCommandHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Unit> Handle(InitiatePasswordResetCommand request, CancellationToken cancellationToken)
    {
        var passwordResetToken = await _authService.InitiatePasswordResetAsync(request.UserEmail);
        
        // password reset token event

        return Unit.Value;
    }
}
