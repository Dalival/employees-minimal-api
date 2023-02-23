namespace EmployeesManagement.RestApi.Contracts.Data;

public class EmployeeDto
{
    public int Id { get; set; }

    public required string FirstName { get; init; }

    public required string SecondName { get; init; }

    public string? Patronymic { get; init; }

    public DateTime? DateOfBirth { get; init; }

    public IList<PositionDto> Positions { get; init; } = new List<PositionDto>();
}
