using Microsoft.Extensions.Logging;

namespace SmingCode.Utilities.ProcessTracking.WebApi;

internal class ProcessTrackingHeaderMiddleware(
    RequestDelegate _next,
    ILogger<ProcessTrackingHeaderMiddleware> _logger
)
{
    public async Task InvokeAsync(
        HttpContext httpContext,
        IProcessTrackingManager processTrackingManager
    )
    {
        var processTrackingDetail = ProcessTrackingHeadersHelper.GetProcessTrackingDetailFromHeader(
            httpContext.Request,
            _logger
        );

        using var activity = processTrackingManager.InitializeProcessActivity(
            "Will add soon",
            System.Diagnostics.ActivityKind.Internal,
            processTrackingDetail
        );

        await _next(httpContext);
    }
}