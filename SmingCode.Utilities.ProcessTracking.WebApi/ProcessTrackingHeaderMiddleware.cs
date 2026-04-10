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
    private readonly ActivitySource _activitySource = new(
        $"{serviceMetadataProvider.GetMetadata().FullServiceDescriptor}.ApiHandler"
    );

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

        var activityContext = Activity.Current?.Context
            ?? new ActivityContext(new(), new(), ActivityTraceFlags.None);

        using (var activity = _activitySource.StartActivity(
            "Handling api",
            ActivityKind.Internal,
            activityContext,
            processTrackingDetail.GetActivityTags()
        ))
        {
            await _next(httpContext);
        }

    }
}