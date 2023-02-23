namespace EmployeesManagement.RestApi.Contracts.Data;

public class PositionDto
{
    public int Id { get; set; }

    public required string Title { get; init; }

    public required int Level { get; init; }
}
