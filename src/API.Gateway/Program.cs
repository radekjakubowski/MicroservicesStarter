using static AuthProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultAuthentication();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetRequiredSection("ServicesRoutes"));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapReverseProxy();

app.Run();
