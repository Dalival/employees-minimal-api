using FastEndpoints;

namespace EmployeesManagement.RestApi.Contracts.Requests.Position;

public class GetPositionRequest
{
    [BindFrom("id")]
    public int PositionId { get; init; }
}
