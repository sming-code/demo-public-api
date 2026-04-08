using Azure.Monitor.OpenTelemetry.AspNetCore;
using DemoApp.PublicApi.BusinessLogic;
using SmingCode.Utilities.MinimalApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.InitialiseBusinessLogic(builder.Configuration);

builder.Services.AddOpenTelemetry().UseAzureMonitor();

var app = builder.Build();

app.MapEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.Run();
