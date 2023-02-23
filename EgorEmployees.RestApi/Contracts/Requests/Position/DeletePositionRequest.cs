using FastEndpoints;

namespace EgorEmployees.RestApi.Contracts.Requests.Position;

public class DeletePositionRequest
{
    [BindFrom("id")]
    public int PositionId { get; init; }
}
