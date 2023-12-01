using MediatR;

namespace Application.Commands.PasswordReset;

public sealed record InitiatePasswordResetCommand(string UserEmail) : IRequest<Unit>;
