namespace EgorEmployees.RestApi.Contracts.Responses;

public class EmployeeResponse
{
    public int Id { get; init; }

    public required string FirstName { get; init; }

    public required string SecondName { get; init; }

    public string? Patronymic { get; init; }

    public DateOnly? DateOfBirth { get; init; }

    public IEnumerable<PositionResponse> Positions { get; init; } = Enumerable.Empty<PositionResponse>();
}
