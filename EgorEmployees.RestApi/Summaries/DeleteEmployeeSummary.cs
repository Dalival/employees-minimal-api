using EgorEmployees.RestApi.Endpoints.Employee;

using FastEndpoints;

namespace EgorEmployees.RestApi.Summaries;

public class DeleteEmployeeSummary : Summary<DeleteEmployeeEndpoint>
{
    public DeleteEmployeeSummary()
    {
        Summary = "Deletes a single employee by id.";
        Description = "Deletes a single employee by id.";
        Response(204, "The employee was deleted successfully.");
        Response(404, "The employee was not found in the system.");
    }
}
