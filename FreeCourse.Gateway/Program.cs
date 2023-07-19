using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_gateway";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddOcelot();
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables(); //TODO: Configuration kullanılabilir mi? Araştıralacak!
});

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
