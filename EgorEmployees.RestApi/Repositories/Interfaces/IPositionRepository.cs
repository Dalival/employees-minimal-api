using EgorEmployees.RestApi.Contracts.Data;

namespace EgorEmployees.RestApi.Repositories.Interfaces;

public interface IPositionRepository : IRepository<int, PositionDto>
{
    Task<bool> IsPositionInUse(int positionId);
}
