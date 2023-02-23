using FastEndpoints;

namespace EmployeesManagement.RestApi.Contracts.Requests.Employee;

public class GetEmployeeRequest
{
    [BindFrom("id")]
    public int EmployeeId { get; init; }
}
