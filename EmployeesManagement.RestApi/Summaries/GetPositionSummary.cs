using EmployeesManagement.RestApi.Contracts.Responses;
using EmployeesManagement.RestApi.Endpoints.Position;

using FastEndpoints;

namespace EmployeesManagement.RestApi.Summaries;

public class GetPositionSummary : Summary<GetPositionEndpoint>
{
    public GetPositionSummary()
    {
        Summary = "Returns a single position by id.";
        Description = "Returns a single position by id.";
        Response<PositionResponse>(200, "Successfully found and returned the position.");
        Response(404, "The position with provided id does not exist in the system.");
    }
}
