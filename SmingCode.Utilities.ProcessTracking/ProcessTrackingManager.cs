using System.Diagnostics;

namespace SmingCode.Utilities.ProcessTracking;

internal class ProcessTrackingManager(
    ProcessTrackingConfiguration processTrackingConfiguration,
    IProcessTrackingHandler _processTrackingHandler
) : IProcessTrackingManager
{
    private const string PROCESS_ID_TAG_NAME = "process-id";
    private const string CORRELATION_ID_TAG_NAME = "correlation-id";
    private readonly ActivitySource _activitySource = new(
        $"{processTrackingConfiguration.ServiceName}.{processTrackingConfiguration.ServiceInstanceId}"
    );

    public Activity InitializeProcessActivity(
        string activityName,
        ActivityKind activityKind,
        ProcessTrackingDetail processTrackingDetail
    )
    {
        _processTrackingHandler.SetProcessTrackingDetail(processTrackingDetail);
        
        var activity = _activitySource.StartActivity(
            activityName,
            activityKind
        );

        activity!.SetTag(PROCESS_ID_TAG_NAME, processTrackingDetail.ProcessId);
        activity.SetTag(CORRELATION_ID_TAG_NAME, processTrackingDetail.CorrelationId);

        return activity;
    }
}
