using MediatR;

namespace Application.Commands.ConfirmEmail;

public sealed record ConfirmEmailCommand(string emailConfirmationToken) : IRequest;
