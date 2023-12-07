using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OddajGlos.Users.Infrastructure.Services.Infrastructure;
using System.Reflection;
using Common;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDefaultAuthentication();
builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<ISecureTokenService, SecureTokenService>();
builder.Services.AddTransient<ITokensService, TokensService>();
builder.Services.AddDbContext<UsersDbContext>(x =>
{
    // connect to dockerized instance of mssql - watch out for connection string as being in dev container u gotta use url from docker compose :D
    var connectionString = builder.Configuration.GetConnectionString("UsersDb");
    x.UseSqlServer(connectionString, opts => {
        opts.EnableRetryOnFailure();
    });
});

builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<UsersDbContext>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.SetupMessageBroker();

var app = builder.Build();

app.EnsureMigrationOfContext<UsersDbContext>();
await app.Services.SeedRoles<UsersDbContext, ApplicationUser>(AuthProvider.Roles);

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
