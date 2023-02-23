using FastEndpoints;

namespace EgorEmployees.RestApi.Contracts.Requests.Employee;

public class DeleteEmployeeRequest
{
    [BindFrom("id")]
    public int EmployeeId { get; init; }
}
