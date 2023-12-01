using Microsoft.AspNetCore.Identity;
using Domain.Exceptions;
using Domain.Abstractions;
using Domain.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Common;
using Infrastructure.Services.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokensService _tokensService;

    public AuthService(UserManager<ApplicationUser> userManager, ITokensService tokensService)
    {
        _userManager = userManager;
        _tokensService = tokensService;
    }

    public async Task ConfirmEmailAsync(string emailConfirmationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == emailConfirmationToken);

        if (user is null)
            throw new EmailConfirmationFailedException(ExceptionDetails.Create(StatusCodes.Status400BadRequest, "Wrong email confirmation link"));

        if (user.EmailConfirmed)
            throw new EmailAlreadyConfirmedException(ExceptionDetails.Create(StatusCodes.Status400BadRequest, "Email already confirmed"));

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
    }

    public async Task<SignInDTO> RefreshTokenAsync(string refreshToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken != null && u.RefreshToken == refreshToken && u.RefreshTokenValidTo! >= DateTime.UtcNow);

        if (user is null)
            throw new RefreshTokenInvalidException(ExceptionDetails.Create(StatusCodes.Status400BadRequest, "Refresh token is invalid or expired"));

        var authClaims = await PrepareAuthClaims(user);
        var bearer = _tokensService.IssueBearerToken(authClaims);
        var newRefreshToken = _tokensService.IssueSecureToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenValidTo = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        return new SignInDTO(bearer, newRefreshToken, user.RefreshTokenValidTo.Value);
    }

    public async Task<SignInDTO> SignInAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new UserNotFoundException(ExceptionDetails.Create(StatusCodes.Status404NotFound, "User not found"));

        if (!await _userManager.CheckPasswordAsync(user, password))
            throw new WrongPasswordException(ExceptionDetails.Create(StatusCodes.Status400BadRequest, "Wrong password"));

        var authClaims = await PrepareAuthClaims(user);
        var bearer = _tokensService.IssueBearerToken(authClaims);
        var refreshToken = _tokensService.IssueSecureToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenValidTo = DateTime.UtcNow.AddDays(7);

        await _userManager.UpdateAsync(user);

        return new SignInDTO(bearer, refreshToken, user.RefreshTokenValidTo.Value);
    }

    public async Task<SignInDTO> SignUpAsync(SignUpDTO signUpDto)
    {
        if (signUpDto.Password != signUpDto.ConfirmPassword)
            throw new PasswordsMustMatchException(ExceptionDetails.Create(StatusCodes.Status400BadRequest, "Passwords must match"));

        if (await _userManager.Users.AnyAsync(x => x.NormalizedEmail == signUpDto.Email.ToUpper()))
            throw new EmailAlreadyTakenException(ExceptionDetails.Create(StatusCodes.Status400BadRequest, "Email is already taken"));

        var newUser = new ApplicationUser
        {
            Email = signUpDto.Email,
            NormalizedEmail = signUpDto.Email.ToUpper(),
            UserName = signUpDto.Email,
            NormalizedUserName = signUpDto.Email.ToUpper(),
            EmailConfirmed = false,
            SecurityStamp = Guid.NewGuid().ToString("D"),
            EmailConfirmationToken = _tokensService.IssueSecureToken()
        };

        await _userManager.CreateAsync(newUser, signUpDto.Password);

        var createdUser = await _userManager.FindByEmailAsync(newUser.Email);
        await _userManager.AddToRoleAsync(createdUser, Roles.User);

        var signIn = await SignInAsync(signUpDto.Email, signUpDto.Password);

        return signIn with { EmailConfirmationToken = createdUser.EmailConfirmationToken! };
    }

    private async Task<List<Claim>> PrepareAuthClaims(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        authClaims.AddRange(userRoles.Select(x => { return new Claim(ClaimTypes.Role, x); }));

        return authClaims;
    }

    public async Task<string> InitiatePasswordResetAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);

        if (user is null)
            throw new UserNotFoundException(ExceptionDetails.Create(StatusCodes.Status404NotFound, "User not found"));

        var passwordResetToken = _tokensService.IssueSecureToken();

        user.PasswordResetToken = passwordResetToken;
        user.PasswordResetTokenValidTo = DateTime.UtcNow.AddMinutes(15);

        await _userManager.UpdateAsync(user);
        return passwordResetToken;
    }

    public async Task ResetPasswordAsync(string passwordResetToken, PasswordResetDTO passwordResetDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.PasswordResetToken != null && u.PasswordResetToken == passwordResetToken && u.PasswordResetTokenValidTo! >= DateTime.UtcNow);

        if (user is null)
            throw new PasswordResetTokenInvalidException(ExceptionDetails.Create(StatusCodes.Status400BadRequest, "Password reset token is invalid or expired"));

        if (passwordResetDto.Password != passwordResetDto.ConfirmPassword)
            throw new PasswordsMustMatchException(ExceptionDetails.Create(StatusCodes.Status400BadRequest, "Passwords must match"));

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, passwordResetDto.Password);
        user.PasswordResetToken = null;
        user.PasswordResetTokenValidTo = null;

        await _userManager.UpdateAsync(user);
    }
}
