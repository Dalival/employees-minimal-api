using EmployeesManagement.RestApi.Mappers;
using EmployeesManagement.RestApi.Domain;
using EmployeesManagement.RestApi.Repositories.Interfaces;
using EmployeesManagement.RestApi.Services.Interfaces;

namespace EmployeesManagement.RestApi.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        var employeesDto = await _employeeRepository.GetAllAsync();
        return employeesDto.Select(e => e.ToEmployee());
    }

    public async Task<Employee?> GetAsync(int id)
    {
        var employeeDto = await _employeeRepository.GetAsync(id);
        return employeeDto?.ToEmployee();
    }

    public async Task<Employee?> CreateAsync(Employee employee)
    {
        var newEmployeeId = await _employeeRepository.CreateAsync(employee.ToEmployeeDto());
        var newEmployeeDto = await _employeeRepository.GetAsync(newEmployeeId);
        return newEmployeeDto?.ToEmployee();
    }

    public async Task<Employee?> UpdateAsync(Employee employee)
    {
        await _employeeRepository.UpdateAsync(employee.ToEmployeeDto());
        var updatedEmployeeDto = await _employeeRepository.GetAsync(employee.Id);
        return updatedEmployeeDto?.ToEmployee();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var deleted = await _employeeRepository.DeleteAsync(id);
        return deleted;
    }
}
