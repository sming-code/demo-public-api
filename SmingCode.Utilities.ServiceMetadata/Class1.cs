using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmingCode.Utilities.ServiceMetadata;

public interface IServiceMetadataProvider
{
    Metadata GetMetadata();
}

internal class ServiceMetadataProvider(
    string serviceName
) : IServiceMetadataProvider
{
    private readonly Metadata _metadata = new(
        serviceName,
        Guid.NewGuid()
    );

    public Metadata GetMetadata() => _metadata;
}

public record Metadata(
    string ServiceName,
    Guid ServiceInstanceId
);

public static class Injection
{
    public static IServiceCollection InitializeServiceMetadata(
        this IServiceCollection services
    )
    {
        var serviceName = ConfigurationManager.AppSettings[]
        return services;
    }
}
