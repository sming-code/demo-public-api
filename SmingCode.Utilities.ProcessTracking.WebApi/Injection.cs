using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SmingCode.Utilities.ProcessTracking.WebApi;

public static class Injection
{
    public static IValidProcessTrackingBuilder AddApiMiddleware(
        this IProcessTrackingBuilder builder
    )
    {
        var services = ((IProcessTrackingBuilderInternal)builder).Services;

        services.AddSingleton<IProcessTrackingHandler, ProcessTrackingHandler>();

        return new ValidProcessTrackingBuilder(services);
    }

    public static IApplicationBuilder UseProcessTrackingMiddleware(
        this IApplicationBuilder applicationBuilder
    )
    {
        applicationBuilder.UseMiddleware<ProcessTrackingHeaderMiddleware>();

        return applicationBuilder;
    }
}