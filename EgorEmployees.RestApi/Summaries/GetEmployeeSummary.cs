using EgorEmployees.RestApi.Contracts.Responses;
using EgorEmployees.RestApi.Endpoints.Employee;

using FastEndpoints;

namespace EgorEmployees.RestApi.Summaries;

public class GetEmployeeSummary : Summary<GetEmployeeEndpoint>
{
    public GetEmployeeSummary()
    {
        Summary = "Returns a single employee by id.";
        Description = "Returns a single employee by id.";
        Response<EmployeeResponse>(200, "Successfully found and returned the employee.");
        Response(404, "The employee with provided id does not exist in the system.");
    }
}
