using EgorEmployees.RestApi.Contracts.Requests.Employee;
using EgorEmployees.RestApi.Contracts.Requests.Position;
using EgorEmployees.RestApi.Domain;

namespace EgorEmployees.RestApi.Mappers;

public static class ApiContractToDomainMapper
{
    public static Employee ToEmployee(this SaveEmployeeRequest request)
    {
        return new Employee
        {
            Id = request.EmployeeId,
            FirstName = request.FirstName,
            SecondName = request.SecondName,
            Patronymic = request.Patronymic,
            DateOfBirth = request.DateOfBirth,
            Positions = request.PositionsIds?.Select(x => new Position { Id = x })
        };
    }

    public static Position ToPosition(this SavePositionRequest request)
    {
        return new Position
        {
            Id = request.PositionId,
            Title = request.Title,
            Level = request.Level
        };
    }
}
