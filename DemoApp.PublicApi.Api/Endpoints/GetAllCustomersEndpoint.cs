namespace DemoApp.PublicApi.Api.Endpoints;
using Models;

public class GetAllCustomersEndpoint : IMinimalEndpoint
{
    public void MapEndpoint(WebApplication app) =>
        app.MapGet(
            "customer",
            async (
                [FromServices] ICustomerService customerService,
                [FromServices] ILogger<GetAllCustomersEndpoint> logger
            ) =>
            {
                logger.LogInformation("Attempting to get customer info.");
                
                var allCustomers = await customerService.GetAllCustomers();

                var response = allCustomers
                    .Select(customer => customer.ToModel());

                // var response = FakeStorage.GetAll();

                return Results.Ok(
                    response
                );
            }
        )
        .WithGroupName("Customers")
        .WithName("GetAllCustomers")
        .Produces<CustomerModel[]>();
}
