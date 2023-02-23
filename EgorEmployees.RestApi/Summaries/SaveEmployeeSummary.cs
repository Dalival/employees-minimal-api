using EgorEmployees.RestApi.Contracts.Responses;
using EgorEmployees.RestApi.Endpoints.Employee;

using FastEndpoints;

namespace EgorEmployees.RestApi.Summaries;

public class SaveEmployeeSummary : Summary<SaveEmployeeEndpoint>
{
    public SaveEmployeeSummary()
    {
        Summary = "Creates or updates an employee.";
        Description = "Creates new employee or updates an existing employee and his/her positions.";
        Response<EmployeeResponse>(201, "Employee was successfully created.");
        Response(204, "Employee was successfully updated.");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks.");
        Response(404, "The employee with provided id was not found or some of provided positions were not found.");
    }
}
