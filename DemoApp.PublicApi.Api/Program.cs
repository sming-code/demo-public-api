using DemoApp.PublicApi.BusinessLogic;
using SmingCode.Utilities.Logging.AspNetCore;
using SmingCode.Utilities.ProcessTracking;
using SmingCode.Utilities.ProcessTracking.WebApi.Config;
using SmingCode.Utilities.ServiceMetadata.Config;
using SmingCode.Utilities.StartupProcesses;
using SmingCode.Utilities.StartupProcesses.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();

services.InitializeServiceMetadata();
builder.InitializeLogging();

services.InitialiseBusinessLogic(builder.Configuration);

services.AddProcessTracking(tracking => 
    tracking.AddApiMiddleware()
);

var app = builder.Build();

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

await app.RunUserDefinedStartupProcesses(
    dependencyManager => dependencyManager.EnableAspNetCore()
);

using var globalLoggerScope = app.Logger.BeginScope(new Dictionary<string, object>
{
    { "service-name", "Public Api" },
    { "service-instance-id", Guid.NewGuid().ToString() }
});

app.Run();
