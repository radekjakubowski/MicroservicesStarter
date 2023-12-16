var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDefaultAuthentication();
builder.Services.AddReverseProxy().LoadFromConfig(builder.Configuration.GetRequiredSection("ServicesRoutes"));

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapReverseProxy();
app.UseCors((policy) => {
  var allowedOrigin = builder.Configuration["AllowedOrigin"];
  policy.WithOrigins(allowedOrigin)
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.Run();
