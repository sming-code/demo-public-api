using SmingCode.Utilities.ServiceApiClient;

namespace DemoApp.PublicApi.Dependencies.Apis.Customer;

internal class CustomerApi(
    IServiceApiClient<CustomerApi> _serviceApiClient
) : ICustomerApi
{
    public async Task<Guid> CreateCustomer(
        string firstName,
        string surname
    ) => await _serviceApiClient.Post<(string FirstName, string Surname), Guid>(
            string.Empty,
            ( firstName, surname )
        );

    public async Task<CustomerDto[]> GetAllCustomers()
     => await _serviceApiClient.Get<CustomerDto[]>(
            string.Empty
        );

    public async Task<CustomerDto> GetCustomerByIdentifier(
        Guid customerIdentifier
    ) => await _serviceApiClient.Get<CustomerDto>(
            $"customer/{customerIdentifier}"
        );
}