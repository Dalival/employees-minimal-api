namespace EgorEmployees.RestApi.Domain;

public class Position
{
    public int Id { get; init; }

    public string Title { get; init; } = default!;

    public int Level { get; init; }
}
