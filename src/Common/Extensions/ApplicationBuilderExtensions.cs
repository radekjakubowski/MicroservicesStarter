﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class ApplicationBuilderExtensions
{
  public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
  {
    using (var scope = app.ApplicationServices.CreateScope())
    {
      var context = scope.ServiceProvider.GetRequiredService<T>();

      try {
        context.Database.Migrate();
      } catch {
        
      }
    }
  }
}
