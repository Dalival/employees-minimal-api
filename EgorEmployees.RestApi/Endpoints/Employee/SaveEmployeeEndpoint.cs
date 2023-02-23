using EgorEmployees.RestApi.Contracts.Requests.Employee;
using EgorEmployees.RestApi.Contracts.Responses;
using EgorEmployees.RestApi.Mappers;
using EgorEmployees.RestApi.Services.Interfaces;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace EgorEmployees.RestApi.Endpoints.Employee;

[HttpPut("employees"), AllowAnonymous]
public class SaveEmployeeEndpoint : Endpoint<SaveEmployeeRequest, EmployeeResponse>
{
    private readonly IEmployeeService _employeeService;

    public SaveEmployeeEndpoint(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public override async Task HandleAsync(SaveEmployeeRequest request, CancellationToken ct)
    {
        if (request.EmployeeId != default)
        {
            await UpdateExistingEmployee(request, ct);
            return;
        }

        await CreateNewEmployee(request, ct);
    }

    private async Task CreateNewEmployee(SaveEmployeeRequest request, CancellationToken ct)
    {
        var employee = request.ToEmployee();
        var newEmployee = await _employeeService.CreateAsync(employee);

        if (newEmployee is null)
        {
            //TODO not the best solution in this case...
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var response = newEmployee.ToEmployeeResponse();
        await SendCreatedAtAsync<GetEmployeeEndpoint>(
            new { response.Id }, response, generateAbsoluteUrl: true, cancellation: ct);
    }

    private async Task UpdateExistingEmployee(SaveEmployeeRequest request, CancellationToken ct)
    {
        var employee = request.ToEmployee();
        var updatedEmployee = await _employeeService.UpdateAsync(employee);

        if (updatedEmployee is null)
        {
            // Employee with provided Id was not found. Or some of PositionsId were not found.
            await SendNotFoundAsync(cancellation: ct);
        }

        await SendNoContentAsync(ct);
    }
}
