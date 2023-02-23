namespace EgorEmployees.RestApi.Contracts.Responses;

public class PositionResponse
{
    public int Id { get; set; }

    public required string Title { get; init; }

    public required int Level { get; init; }
}
