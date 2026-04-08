namespace DemoApp.PublicApi.Domain.Dependencies;
using Dtos;

public interface ICustomerApi
{
    Task<Guid> CreateCustomer(
        string firstName,
        string surname
    );
    Task<CustomerDto[]> GetAllCustomers();
    Task<CustomerDto> GetCustomerByIdentifier(
        Guid customerIdentifier
    );
}
