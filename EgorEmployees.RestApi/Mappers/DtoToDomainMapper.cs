using EgorEmployees.RestApi.Contracts.Data;
using EgorEmployees.RestApi.Domain;

namespace EgorEmployees.RestApi.Mappers;

public static class DtoToDomainMapper
{
    public static Employee ToEmployee(this EmployeeDto employeeDto)
    {
        return new Employee
        {
            Id = employeeDto.Id,
            FirstName = employeeDto.FirstName,
            SecondName = employeeDto.SecondName,
            Patronymic = employeeDto.Patronymic,
            DateOfBirth = employeeDto.DateOfBirth.HasValue
                ? DateOnly.FromDateTime(employeeDto.DateOfBirth.Value)
                : null,
            Positions = employeeDto.Positions.Select(p => p.ToPosition())
        };
    }

    public static Position ToPosition(this PositionDto positionDto)
    {
        return new Position
        {
            Id = positionDto.Id,
            Title = positionDto.Title,
            Level = positionDto.Level
        };
    }
}
