using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Common;

public static class ServiceCollectionExtensions
{
    public static async Task SeedRoles<TDbContext, TUserType>(this IServiceProvider serviceProvider, IList<string> roles) where TDbContext : IdentityDbContext<TUserType> where TUserType : IdentityUser
    {
        using var scope = serviceProvider.CreateScope();

        var identityDbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        var roleStore = new RoleStore<IdentityRole>(identityDbContext);

        if (roleStore.Roles.Count() != 0) return;

        foreach (var role in roles)
        {
            await roleStore.CreateAsync(new IdentityRole { Name = role, NormalizedName = role.ToUpper(), ConcurrencyStamp = Guid.NewGuid().ToString("D") });
        }

        await identityDbContext.SaveChangesAsync();
    }

    public static void SetupMessageBroker(this IServiceCollection services, Action<IRabbitMqReceiveEndpointConfigurator> consumersConfig = null) {
        services.AddMassTransit(cfg => {
            cfg.UsingRabbitMq((context, config) => {
                config.Host("rabbitmq", opts => {
                    opts.Username("user");
                    opts.Password("password");
                });

                config.ReceiveEndpoint(consumersConfig);
            });
        });
    }
}
