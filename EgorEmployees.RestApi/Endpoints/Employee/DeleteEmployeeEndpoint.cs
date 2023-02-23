using EgorEmployees.RestApi.Contracts.Requests.Employee;
using EgorEmployees.RestApi.Services.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace EgorEmployees.RestApi.Endpoints.Employee;

[HttpDelete("employees/{id}"), AllowAnonymous] //todo check do I need {id:int}
public class DeleteEmployeeEndpoint : Endpoint<DeleteEmployeeRequest>
{
    private readonly IEmployeeService _employeeService;

    public DeleteEmployeeEndpoint(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public override async Task HandleAsync(DeleteEmployeeRequest request, CancellationToken ct)
    {
        var deleted = await _employeeService.DeleteAsync(request.EmployeeId);

        if (!deleted)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}
