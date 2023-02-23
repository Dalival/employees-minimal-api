using EmployeesManagement.RestApi.Endpoints.Position;

using FastEndpoints;

namespace EmployeesManagement.RestApi.Summaries;

public class DeletePositionSummary : Summary<DeletePositionEndpoint>
{
    public DeletePositionSummary()
    {
        Summary = "Deletes a single position by id.";
        Description = "Deletes a single position by id.";
        Response(204, "The position was deleted successfully.");
        Response(404, "The position was not found in the system.");
        Response(403, "The position cannot be deleted because some employees are assigned to it.");
    }
}
