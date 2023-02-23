namespace EgorEmployees.RestApi.Contracts.Requests.Position;

public class SavePositionRequest
{
    public int PositionId { get; init; }

    public string Title { get; init; } = default!;

    public int Level { get; init; }
}
