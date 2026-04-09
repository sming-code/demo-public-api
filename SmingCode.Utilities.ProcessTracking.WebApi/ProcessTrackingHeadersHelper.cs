using Microsoft.Extensions.Logging;

namespace SmingCode.Utilities.ProcessTracking.WebApi;

internal static class ProcessTrackingHeadersHelper
{
    internal static ProcessTrackingDetail GetProcessTrackingDetailFromHeader(
        this HttpRequest httpRequest,
        ILogger<ProcessTrackingHeaderMiddleware> logger
    )
    {
        logger.LogInformation("Got in here.");
        logger.LogInformation(
            "Headers are: {HeaderInfo}",
            string.Join(
                ",",
                httpRequest.Headers.Select(header =>
                    $"{header.Key}:{header.Value}"
                )
            )
        );

        if (!(
            httpRequest.Headers.TryGetValue(Configuration.PROCESS_ID_HEADER_NAME, out var processIdHeaderVal)
            && httpRequest.Headers.TryGetValue(Configuration.CORRELATION_ID_HEADER_NAME, out var correlationIdHeaderVal)
        ))
        {
            throw new Exception();
        }

        logger.LogInformation(
            "Parsed headers are {header1} and {header2}",
            processIdHeaderVal,
            correlationIdHeaderVal
        );

        return new(
            processIdHeaderVal!,
            correlationIdHeaderVal!
        );
    }
}