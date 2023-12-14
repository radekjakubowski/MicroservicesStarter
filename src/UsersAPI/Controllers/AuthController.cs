using Application.Commands.ConfirmEmail;
using Application.Commands.PasswordReset;
using Application.Commands.RefreshToken;
using Application.Commands.SignIn;
using Application.Commands.SignUp;
using Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ping")]
    public ActionResult<string> PingAsync()
    {
        return Ok("pong");
    }

    [HttpPost("sign-in", Name = "SignIn")]
    public async Task<ActionResult<SignInCommandResponse>> SignInAsync(SignInCommand signInCommand)
    {
        return Ok(await _mediator.Send(signInCommand));
    }

    [HttpPost("sign-up", Name = "SignUp")]
    public async Task<ActionResult<SignUpCommandResponse>> SignUpAsync(SignUpCommand signUpCommand)
    {
        return Ok(await _mediator.Send(signUpCommand));
    }

    [HttpPost("refresh-token", Name = "RefreshToken")]
    public async Task<ActionResult<RefreshTokenCommandResponse>> RerfeshToken(RefreshTokenCommand refreshTokenCommand)
    {
        return Ok(await _mediator.Send(refreshTokenCommand));
    }

    [HttpPut("confirm-email/{confirmationToken}", Name = "ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync(string confirmationToken)
    {
        await _mediator.Send(new ConfirmEmailCommand(confirmationToken));
        return Ok("Email confirmed succesfully");
    }

    [HttpPut("password-reset", Name = "InitiatePasswordReset")]
    public async Task<IActionResult> InitiatePasswordReset(string userEmail)
    {
        await _mediator.Send(new InitiatePasswordResetCommand(userEmail));
        return Accepted("Password reset initiated successfully");
    }

    [HttpPut("password-reset/token={passwordResetToken}", Name = "PasswordReset")]
    public async Task<IActionResult> ResetPassword(string passwordResetToken, PasswordResetDTO passwordResetDto)
    {
        await _mediator.Send(new PasswordResetCommand(passwordResetToken, passwordResetDto));
        return Ok("Password reset success. Log in with your new credentials");
    }
}