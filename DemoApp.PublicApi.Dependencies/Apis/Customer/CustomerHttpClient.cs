using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using SmingCode.Utilities.ProcessTracking.WebApi;

namespace DemoApp.PublicApi.Dependencies.Apis.Customer;

internal class CustomerHttpClient(
    HttpClient _httpClient,
    IProcessTrackingHeadersProvider _processTrackingHeadersProvider
)
{
    internal async Task<CustomerDto[]> GetAll()
    {
        HttpRequestMessage request = new(
            HttpMethod.Get,
            "customer"
        );

        AddProcessTrackingHeaders(request);
        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CustomerDto[]>();
        return result!;
    }

    internal async Task<Guid> CreateCustomer(
        string firstName,
        string surname
    )
    {
        HttpRequestMessage request = new(
            HttpMethod.Post,
            "customer"
        );

        AddProcessTrackingHeaders(request);

        request.Content = new StringContent(
            JsonSerializer.Serialize(
                new
                {
                    FirstName = firstName,
                    Surname = surname
                },
                JsonSerializerOptions.Web
            )
        );

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Guid>();
        return result!;
    }

    internal async Task<CustomerDto> GetById(
        Guid customerId
    )
    {
        HttpRequestMessage request = new(
            HttpMethod.Get,
            $"customer/{customerId}"
        );

        AddProcessTrackingHeaders(request);
        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CustomerDto>();
        return result!;
    }

    private void AddProcessTrackingHeaders(
        HttpRequestMessage httpRequestMessage
    )
    {
        var requiredHeaders = _processTrackingHeadersProvider.GetProcessTrackingHeaders();

        foreach (var processTrackingHeader in requiredHeaders)
        {
            httpRequestMessage.Headers.Add(processTrackingHeader.Key, processTrackingHeader.Value);
        }
    }
}