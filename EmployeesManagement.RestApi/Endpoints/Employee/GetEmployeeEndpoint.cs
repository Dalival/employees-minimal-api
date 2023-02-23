using EmployeesManagement.RestApi.Mappers;
using EmployeesManagement.RestApi.Contracts.Requests.Employee;
using EmployeesManagement.RestApi.Contracts.Responses;
using EmployeesManagement.RestApi.Services.Interfaces;

using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesManagement.RestApi.Endpoints.Employee;

[HttpGet("employees/{id}"), AllowAnonymous] //todo check do I need {id:int}
public class GetEmployeeEndpoint : Endpoint<GetEmployeeRequest, EmployeeResponse>
{
    private readonly IEmployeeService _employeeService;

    public GetEmployeeEndpoint(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public override async Task HandleAsync(GetEmployeeRequest request, CancellationToken ct)
    {
        if (request.EmployeeId <= 0)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var employee = await _employeeService.GetAsync(request.EmployeeId);

        if (employee is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = employee.ToEmployeeResponse();
        await SendOkAsync(response, ct);
    }
}
