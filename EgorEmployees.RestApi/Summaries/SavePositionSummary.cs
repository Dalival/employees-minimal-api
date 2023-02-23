using EgorEmployees.RestApi.Contracts.Responses;
using EgorEmployees.RestApi.Endpoints.Position;

using FastEndpoints;

namespace EgorEmployees.RestApi.Summaries;

public class SavePositionSummary : Summary<SavePositionEndpoint>
{
    public SavePositionSummary()
    {
        Summary = "Creates or updates a position.";
        Description = "Creates new position or updates an existing position.";
        Response<PositionResponse>(201, "Position was successfully created.");
        Response(204, "Position was successfully updated.");
        Response<ValidationFailureResponse>(400, "The request did not pass validation checks.");
        Response(404, "The position with provided id was not found.");
    }
}
