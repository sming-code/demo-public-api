using System.Diagnostics;

namespace SmingCode.Utilities.ProcessTracking;

internal interface IProcessTrackingManager
{
    Activity InitializeProcessActivity(
        ActivityKind activityKind,
        ProcessTrackingDetail processTrackingDetail
    );
}
