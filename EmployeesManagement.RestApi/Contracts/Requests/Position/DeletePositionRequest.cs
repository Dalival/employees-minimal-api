using FastEndpoints;

namespace EmployeesManagement.RestApi.Contracts.Requests.Position;

public class DeletePositionRequest
{
    [BindFrom("id")]
    public int PositionId { get; init; }
}
