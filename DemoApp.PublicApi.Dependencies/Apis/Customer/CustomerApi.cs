using SmingCode.Utilities.ServiceApiClient;

namespace DemoApp.PublicApi.Dependencies.Apis.Customer;
using Models;

internal class CustomerApi(
    IServiceApiClient<CustomerApi> _serviceApiClient
) : ICustomerApi
{
    public async Task<Guid> CreateCustomer(
        string firstName,
        string surname
    ) => await _serviceApiClient.Post<CreateCustomerModel, Guid>(
            string.Empty,
            new (firstName, surname)
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