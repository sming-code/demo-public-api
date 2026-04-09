using System.Diagnostics;

namespace SmingCode.Utilities.ProcessTracking;

internal interface IProcessTrackingManager
{
    Activity InitializeProcessActivity(
        string activityName,
        ActivityKind activityKind,
        ProcessTrackingDetail processTrackingDetail
    );
}
