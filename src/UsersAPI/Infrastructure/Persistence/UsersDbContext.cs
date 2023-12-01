using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UsersDbContext : IdentityDbContext<ApplicationUser>
{
  public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
  {
  }
}