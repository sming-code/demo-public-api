using Microsoft.Extensions.Configuration;
using SmingCode.Utilities.ServiceApiClient;

namespace DemoApp.PublicApi.Dependencies;
using Apis.Customer;

public static class DependencyInjection
{
    public static IServiceCollection InitialiseDependencies(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddApiClient<ICustomerApi, CustomerApi>(
            "Customer Service",
            "customer-svc",
            httpClient =>
            {
                httpClient.BaseAddress = new Uri(configuration["Apis:Customer:BaseAddress"]!);
            });
        // services.AddScoped<ICustomerApi, CustomerApi>();

        return services;
    }
}