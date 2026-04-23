using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace DemoApp.PublicApi.Dependencies.Apis.Customer;

internal class CustomerApi(
    HttpClient _httpClient
) : ICustomerApi
{
    public async Task<Guid> CreateCustomer(
        string firstName,
        string surname
    )
    {
        var response = await _httpClient.PostAsJsonAsync(
            "customer",
            new
            {
                FirstName = firstName,
                Surname = surname
            });

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Guid>();
        return result!;
    }

    public async Task<CustomerDto[]> GetAllCustomers()
    {
        HttpRequestMessage request = new(
            HttpMethod.Get,
            "customer"
        );

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CustomerDto[]>();
        return result!;
    }

    public async Task<CustomerDto> GetCustomerByIdentifier(
        Guid customerIdentifier
    )
    {
        var response = await _httpClient.GetAsync(
            $"customer/{customerIdentifier}"
        );

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CustomerDto>();
        return result!;
    }
}