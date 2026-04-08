namespace DemoApp.PublicApi.Dependencies.Apis.Customer;

internal class CustomerApi(
    CustomerHttpClient _httpClient
) : ICustomerApi
{
    public async Task<Guid> CreateCustomer(
        string firstName,
        string surname
    ) => await _httpClient.CreateCustomer(
        firstName,
        surname
    );

    public async Task<CustomerDto[]> GetAllCustomers() => await _httpClient.GetAll();

    public async Task<CustomerDto> GetCustomerByIdentifier(
        Guid customerIdentifier
    ) => await _httpClient.GetById(customerIdentifier);
}