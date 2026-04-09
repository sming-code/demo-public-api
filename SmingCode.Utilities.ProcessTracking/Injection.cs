using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Trace;

namespace SmingCode.Utilities.ProcessTracking;

public static class Injection
{
    public static IServiceCollection AddProcessTracking(
        this IServiceCollection services,
        Func<IProcessTrackingBuilder, IValidProcessTrackingBuilder> initialization
    )
    {
        var processTrackingConfiguration = new ProcessTrackingConfiguration
        {
            ServiceName = "Public Api Api"
        };
        services.AddSingleton(processTrackingConfiguration);

        var processTrackingBuilder = new ProcessTrackingBuilder(services);
        initialization(processTrackingBuilder);

        services.AddSingleton<IProcessTrackingManager, ProcessTrackingManager>();
        services.AddSingleton<ProcessTrackingHandlerManager>();

        return services;
    }
}
