using EmployeesManagement.RestApi.Domain;

namespace EmployeesManagement.RestApi.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>> GetAllAsync();

    Task<Employee?> GetAsync(int id);

    Task<Employee?> CreateAsync(Employee employee);

    Task<Employee?> UpdateAsync(Employee employee);

    Task<bool> DeleteAsync(int id);
}
