namespace SmingCode.Utilities.ServiceMetadata;

public record Metadata(
    string ServiceName,
    Guid ServiceInstanceId
)
{
    public string FullServiceDescriptor => $"{ServiceName}.{ServiceInstanceId}";
};
