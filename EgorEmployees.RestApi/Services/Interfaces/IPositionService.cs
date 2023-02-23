using EgorEmployees.RestApi.Domain;

namespace EgorEmployees.RestApi.Services.Interfaces;

public interface IPositionService
{
    Task<IEnumerable<Position>> GetAllAsync();

    Task<Position?> GetAsync(int id);

    Task<Position?> CreateAsync(Position position);

    Task<Position?> UpdateAsync(Position position);

    Task<bool> DeleteAsync(int id);

    Task<bool> IsPositionInUse(int positionId);
}
