using EgorEmployees.RestApi.Contracts.Data;
using EgorEmployees.RestApi.Domain;

namespace EgorEmployees.RestApi.Mappers;

public static class DomainToDtoMapper
{
    public static EmployeeDto ToEmployeeDto(this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            SecondName = employee.SecondName,
            Patronymic = employee.Patronymic,
            DateOfBirth = employee.DateOfBirth?.ToDateTime(TimeOnly.MinValue),
            Positions = employee.Positions?.Select(p => p.ToPositionDto()).ToList() ?? new List<PositionDto>()
        };
    }

    public static PositionDto ToPositionDto(this Position position)
    {
        return new PositionDto
        {
            Id = position.Id,
            Title = position.Title,
            Level = position.Level
        };
    }
}
