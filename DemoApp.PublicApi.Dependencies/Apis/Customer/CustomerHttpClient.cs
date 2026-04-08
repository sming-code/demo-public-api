using System.Net.Http.Json;
using System.Text.Json;

namespace DemoApp.PublicApi.Dependencies.Apis.Customer;

internal class CustomerHttpClient(
    HttpClient _httpClient
)
{
    internal async Task<CustomerDto[]> GetAll()
    {
        var response = await _httpClient.GetAsync("customer");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CustomerDto[]>();
        return result!;
    }

    internal async Task<Guid> CreateCustomer(
        string firstName,
        string surname
    )
    {
        var response = await _httpClient.PostAsJsonAsync(
            "customer",
            new {
                FirstName = firstName,
                Surname = surname
            }
        );

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<Guid>();
        return result!;
    }

    internal async Task<CustomerDto> GetById(
        Guid customerId
    )
    {
        var response = await _httpClient.GetAsync($"customer/{customerId}");

        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<CustomerDto>();
        return result!;
    }
}