using FastEndpoints;

namespace EgorEmployees.RestApi.Contracts.Requests.Position;

public class GetPositionRequest
{
    [BindFrom("id")]
    public int PositionId { get; init; }
}
