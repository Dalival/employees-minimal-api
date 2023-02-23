using EgorEmployees.RestApi.Contracts.Data;

namespace EgorEmployees.RestApi.Repositories.Interfaces;

public interface IEmployeeRepository : IRepository<int, EmployeeDto>
{
}
