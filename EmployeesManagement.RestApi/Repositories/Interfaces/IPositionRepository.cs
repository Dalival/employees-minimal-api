using EmployeesManagement.RestApi.Contracts.Data;

namespace EmployeesManagement.RestApi.Repositories.Interfaces;

public interface IPositionRepository : IRepository<int, PositionDto>
{
    Task<bool> IsPositionInUse(int positionId);
}
