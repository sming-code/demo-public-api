using Azure.Monitor.OpenTelemetry.AspNetCore;
using Azure.Monitor.OpenTelemetry.Exporter;
using DemoApp.PublicApi.BusinessLogic;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using SmingCode.Utilities.ProcessTracking;
using SmingCode.Utilities.ProcessTracking.WebApi;
using SmingCode.Utilities.ServiceMetadata;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();
services.InitializeServiceMetadata();

builder.Logging.ClearProviders();

// services.AddLogging(builder =>
// {
//     services.AddOpenTelemetry(
//         builder,
//         loggerOptions => loggerOptions.IncludeScopes = true
//     );
// });

services.AddOpenTelemetry()
    .UseAzureMonitorExporter()
    .ConfigureResource(configure => configure.AddService("PublicApiApi"))
    .WithLogging(
        logging => logging.AddAzureMonitorLogExporter(),
        options =>
        {
            options.IncludeScopes = true;
        }
    );

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

app.UseProcessTrackingMiddleware();
// app.UseHttpsRedirection();

app.Run();
