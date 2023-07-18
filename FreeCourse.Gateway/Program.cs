using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddOcelot();
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables(); //TODO: Configuration kullanılabilir mi? Araştıralacak!
});

app.MapGet("/", () => "Hello World!");

await app.UseOcelot();

app.Run();
