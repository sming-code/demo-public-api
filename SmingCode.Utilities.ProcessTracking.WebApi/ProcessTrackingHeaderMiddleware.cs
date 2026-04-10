using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace SmingCode.Utilities.ProcessTracking.WebApi;
using ServiceMetadata;

internal class ProcessTrackingHeaderMiddleware(
    RequestDelegate _next,
    IServiceMetadataProvider serviceMetadataProvider,
    ILogger<ProcessTrackingHeaderMiddleware> _logger
)
{
    public async Task InvokeAsync(
        HttpContext httpContext,
        IProcessTrackingHandler processTrackingHandler
    )
    {
        _logger.LogInformation(
            "Trying to get process tracking detail"
        );
        var processTrackingDetail = ProcessTrackingHeadersHelper.GetProcessTrackingDetailFromHeader(
            httpContext.Request,
            _logger
        );

        processTrackingHandler.SetProcessTrackingDetail(processTrackingDetail);
        _logger.LogInformation(
            "Process tracking detail is {ProcessTrackingDetail}",
            JsonSerializer.Serialize(processTrackingDetail)
        );

        using var scope = _logger.BeginScope(
            processTrackingDetail.GetActivityTags()
                .Concat(serviceMetadataProvider.GetMetadata().GetCustomDimensions())
        );

        await _next(httpContext);
    }
}