using FastEndpoints;

namespace EgorEmployees.RestApi.Contracts.Requests.Employee;

public class GetEmployeeRequest
{
    [BindFrom("id")]
    public int EmployeeId { get; init; }
}
