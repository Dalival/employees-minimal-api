using EmployeesManagement.RestApi.Contracts.Data;

namespace EmployeesManagement.RestApi.Repositories.Interfaces;

public interface IEmployeeRepository : IRepository<int, EmployeeDto>
{
}
