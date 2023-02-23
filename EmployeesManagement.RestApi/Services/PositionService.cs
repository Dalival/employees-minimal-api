using EmployeesManagement.RestApi.Mappers;
using EmployeesManagement.RestApi.Domain;
using EmployeesManagement.RestApi.Repositories.Interfaces;
using EmployeesManagement.RestApi.Services.Interfaces;

namespace EmployeesManagement.RestApi.Services;

public class PositionService : IPositionService
{
    private readonly IPositionRepository _positionRepository;

    public PositionService(IPositionRepository positionRepository)
    {
        _positionRepository = positionRepository;
    }

    public async Task<IEnumerable<Position>> GetAllAsync()
    {
        var positions = await _positionRepository.GetAllAsync();
        return positions.Select(p => p.ToPosition());
    }

    public async Task<Position?> GetAsync(int id)
    {
        var position = await _positionRepository.GetAsync(id);
        return position?.ToPosition();
    }

    public async Task<Position?> CreateAsync(Position position)
    {
        var newPositionId = await _positionRepository.CreateAsync(position.ToPositionDto());
        var newPosition = await _positionRepository.GetAsync(newPositionId);
        return newPosition?.ToPosition();
    }

    public async Task<Position?> UpdateAsync(Position position)
    {
        await _positionRepository.UpdateAsync(position.ToPositionDto());
        return position;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _positionRepository.DeleteAsync(id);
        return deleted;
    }

    public async Task<bool> IsPositionInUse(int positionId)
    {
        var isInUse = await _positionRepository.IsPositionInUse(positionId);
        return isInUse;
    }
}
