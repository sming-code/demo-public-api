namespace DemoApp.PublicApi.Domain.Dtos;

public record CustomerDto(
    Guid CustomerIdentifier,
    string FirstName,
    string Surname
);