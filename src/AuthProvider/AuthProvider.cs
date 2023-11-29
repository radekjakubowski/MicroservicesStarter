using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AuthProvider;

public static class AuthProvider
{
  public static void AddDefaultAuthentication(this IServiceCollection services, ConfigurationManager configuration)
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
           ValidIssuer = configuration["JWT:Issuer"],
           ValidateIssuer = false,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
           ValidateAudience = false,
           ValidAudience = configuration["JWT:Issuer"]
         };
       });
  }
}

