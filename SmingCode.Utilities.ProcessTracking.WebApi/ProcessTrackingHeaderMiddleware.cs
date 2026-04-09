namespace SmingCode.Utilities.ProcessTracking.WebApi;

internal class ProcessTrackingHeaderMiddleware(
    RequestDelegate _next
)
{
    public async Task InvokeAsync(
        HttpContext httpContext,
        IProcessTrackingManager processTrackingManager
    )
    {
        var processTrackingDetail = ProcessTrackingHeadersHelper.GetProcessTrackingDetailFromHeader(
            httpContext.Request
        );

        using var activity = processTrackingManager.InitializeProcessActivity(
            "Will add soon",
            System.Diagnostics.ActivityKind.Internal,
            processTrackingDetail
        );

        await _next(httpContext);
    }
}