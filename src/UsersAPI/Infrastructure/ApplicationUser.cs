using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public class ApplicationUser : IdentityUser
{
  public string? EmailConfirmationToken { get; set; }
  public string? RefreshToken { get; set; }
  public DateTime? RefreshTokenValidTo { get; set; }
  public DateTime? LastVoteDate { get; set; }
  public string? PasswordResetToken { get; set; }
  public DateTime? PasswordResetTokenValidTo { get; set; }
}
