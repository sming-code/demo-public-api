using DemoApp.PublicApi.Domain.Dtos;

namespace DemoApp.PublicApi.BusinessLogic;

internal class CustomerService(
    ICustomerApi _customerApi
) : ICustomerService
{
    public async Task<Guid> CreateCustomer(
        string firstName,
        string surname
    ) => await _customerApi.CreateCustomer(
        firstName,
        surname
    );
    public async Task<CustomerDto[]> GetAllCustomers()
        => await _customerApi.GetAllCustomers();

    public async Task<CustomerDto> GetCustomerByIdentifier(
        Guid customerIdentifier
    ) => await _customerApi.GetCustomerByIdentifier(
        customerIdentifier
    );
}