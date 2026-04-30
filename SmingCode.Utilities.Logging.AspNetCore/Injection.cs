using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Azure.Monitor.OpenTelemetry.AspNetCore;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Trace;
using OpenTelemetry;
using System.Diagnostics;

namespace SmingCode.Utilities.Logging.AspNetCore;

using OpenTelemetry.Resources;
using ServiceMetadata;

public static class Injection
{
    public static WebApplicationBuilder InitializeLogging(
        this WebApplicationBuilder builder
    )
    {
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(r => r.AddService("TestServiceName", serviceVersion: "1.0.1", autoGenerateServiceInstanceId: false, serviceInstanceId: "Test"))
            .UseAzureMonitor();

        builder.Logging.AddOpenTelemetry(options => options.IncludeScopes = true);

        return builder;
    }
}

internal class ServiceMetadataOpenTelemetryActivityEnrichingProcessor : BaseProcessor<Activity>
{
    private static readonly string _instanceId = Guid.NewGuid().ToString();
    public override void OnEnd(Activity activity)
    {
        activity.SetTag("service-name", "Public Api");
        activity.SetTag("service-instance-id", _instanceId);
    }
}