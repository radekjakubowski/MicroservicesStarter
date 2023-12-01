using MediatR;
using Domain.Abstractions;

namespace Application.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
{
    private readonly IAuthService _userService;

    public ConfirmEmailCommandHandler(IAuthService userService)
    {
        _userService = userService;
    }

    public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        await _userService.ConfirmEmailAsync(request.emailConfirmationToken);
    }
}
