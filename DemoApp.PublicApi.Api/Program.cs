using Azure.Monitor.OpenTelemetry.AspNetCore;
using DemoApp.PublicApi.BusinessLogic;
using OpenTelemetry.Trace;
using SmingCode.Utilities.ProcessTracking;
using SmingCode.Utilities.ProcessTracking.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.InitialiseBusinessLogic(builder.Configuration);

builder.Services.AddOpenTelemetry()
    .UseAzureMonitor()
    .WithTracing(
        tracerProviderBuilder => tracerProviderBuilder.AddSource("TestActivitySource")
    );

builder.Services.AddProcessTracking(tracking =>
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
