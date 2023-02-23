namespace EmployeesManagement.RestApi.Domain;

public class Employee
{
    public int Id { get; init; }

    public string FirstName { get; init; } = default!;

    public string SecondName { get; init; } = default!;

    public string? Patronymic { get; init; }

    public DateOnly? DateOfBirth { get; init; }

    public IEnumerable<Position>? Positions { get; init; }
}
