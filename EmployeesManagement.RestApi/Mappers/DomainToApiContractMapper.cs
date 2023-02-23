using EmployeesManagement.RestApi.Contracts.Responses;
using EmployeesManagement.RestApi.Domain;

namespace EmployeesManagement.RestApi.Mappers;

public static class DomainToApiContractMapper
{
    public static EmployeeResponse ToEmployeeResponse(this Employee employee)
    {
        return new EmployeeResponse
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            SecondName = employee.SecondName,
            Patronymic = employee.Patronymic,
            DateOfBirth = employee.DateOfBirth,
            Positions = employee.Positions?.Select(p => p.ToPositionResponse()) ?? Enumerable.Empty<PositionResponse>()
        };
    }

    public static PositionResponse ToPositionResponse(this Position position)
    {
        return new PositionResponse
        {
            Id = position.Id,
            Title = position.Title,
            Level = position.Level
        };
    }
}
