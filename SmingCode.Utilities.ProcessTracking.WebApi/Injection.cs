using Microsoft.AspNetCore.Builder;

namespace SmingCode.Utilities.ProcessTracking.WebApi;

public static class Injection
{
    // public static IValidProcessTrackingBuilder AddApiMiddleware(
    //     this IProcessTrackingBuilder builder
    // )
    // {
    //     var services = ((IProcessTrackingBuilderInternal)builder).Services;

    //     services.AddSingleton<IProcessTrackingHandler, ProcessTrackingHandler>();
    //     services.AddSingleton<IProcessTrackingManager, ProcessTrackingManager>();

    //     return new ValidProcessTrackingBuilder(services);
    // }

    public static IApplicationBuilder UseProcessTrackingMiddleware(
        this IApplicationBuilder applicationBuilder
    )
    {
        applicationBuilder.UseMiddleware<ProcessTrackingHeaderMiddleware>();

        return applicationBuilder;
    }
}