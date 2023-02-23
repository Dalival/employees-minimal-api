using FastEndpoints;

namespace EgorEmployees.RestApi.Contracts.Requests.Employee;

public class SaveEmployeeRequest
{
    public int EmployeeId { get; init; }

    public string FirstName { get; init; } = default!;

    public string SecondName { get; init; } = default!;

    public string? Patronymic { get; init; }

    public DateOnly? DateOfBirth { get; init; }

    public List<int>? PositionsIds { get; init; }
}
