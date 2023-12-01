using MediatR;
using Domain.DTO;

namespace Application.Commands.PasswordReset;

public sealed record PasswordResetCommand(string ResetPasswordToken, PasswordResetDTO PasswordResetDto) : IRequest<Unit>;