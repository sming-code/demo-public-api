namespace SmingCode.Utilities.ProcessTracking.WebApi;

internal static class ProcessTrackingHeadersHelper
{
    internal static ProcessTrackingDetail GetProcessTrackingDetailFromHeader(
        this HttpRequest httpRequest
    )
    {
        if (!(
            httpRequest.Headers.TryGetValue(Configuration.PROCESS_ID_HEADER_NAME, out var processIdHeaderVal)
            && httpRequest.Headers.TryGetValue(Configuration.CORRELATION_ID_HEADER_NAME, out var correlationIdHeaderVal)
        ))
        {
            throw new Exception();
        }

        return new(
            processIdHeaderVal!,
            correlationIdHeaderVal!
        );
    }
}