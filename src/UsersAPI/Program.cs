using Domain.Abstractions;
using Infrastructure;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Infrastructure.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OddajGlos.Users.Infrastructure.Services.Infrastructure;
using System.Reflection;
using static AuthProvider;

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
    // connect to dockerized instance of mssql
    var connectionString = builder.Configuration.GetConnectionString("UsersDb");
    x.UseSqlServer(connectionString);
});

builder.Services
    .AddIdentityCore<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<UsersDbContext>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
