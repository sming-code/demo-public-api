using Microsoft.Extensions.DependencyInjection;

namespace SmingCode.Utilities.ServiceMetadata;

public static class Injection
{
    public static IServiceCollection InitializeServiceMetadata(
        this IServiceCollection services,
        out Metadata serviceMetadata
    )
    {
        var serviceMetadataProvider = new ServiceMetadataProvider();
        serviceMetadata = serviceMetadataProvider.GetMetadata();

        services.AddSingleton<IServiceMetadataProvider>(serviceMetadataProvider);

        return services;
    }
}
