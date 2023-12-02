using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

public static class AuthProvider
{
  public static string JwtSecret = "mysuperfancysecretkeyjwt123!";
  public static string JwtIssuer = "http://microservicesstarter.com";

  public static readonly List<string> Roles = new() { "Admin", "User" };

  public static void AddDefaultAuthentication(this IServiceCollection services)
  {
    services.AddAuthentication(opts =>
       {
         opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
         opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
       })
       .AddJwtBearer(opts =>
       {
         opts.RequireHttpsMetadata = false;
         opts.TokenValidationParameters = new TokenValidationParameters
         {
           ValidIssuer = JwtIssuer,
           ValidateIssuer = false,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret)),
           ValidateAudience = false,
           ValidAudience = JwtIssuer
         };
       });
  }
}

