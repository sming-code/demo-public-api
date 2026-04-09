using System.Text.Json;
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
        _logger.LogInformation(
            "Trying to get process tracking detail"
        );
        var processTrackingDetail = ProcessTrackingHeadersHelper.GetProcessTrackingDetailFromHeader(
            httpContext.Request,
            _logger
        );
        _logger.LogInformation(
            "Process tracking detail is {ProcessTrackingDetail}",
            JsonSerializer.Serialize(processTrackingDetail)
        );

        using var activity = processTrackingManager.InitializeProcessActivity(
            "Will add soon",
            System.Diagnostics.ActivityKind.Internal,
            processTrackingDetail
        );

        await _next(httpContext);
    }
}