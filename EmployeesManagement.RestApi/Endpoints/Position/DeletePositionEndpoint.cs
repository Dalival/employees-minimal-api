using EmployeesManagement.RestApi.Contracts.Requests.Position;
using EmployeesManagement.RestApi.Services.Interfaces;

using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesManagement.RestApi.Endpoints.Position;

[HttpDelete("positions/{id}"), AllowAnonymous] //TODO check do I need {id:int}
public class DeletePositionEndpoint  : Endpoint<DeletePositionRequest>
{
    private readonly IPositionService _positionService;

    public DeletePositionEndpoint(IPositionService positionService)
    {
        _positionService = positionService;
    }

    public override async Task HandleAsync(DeletePositionRequest request, CancellationToken ct)
    {
        var isPositionInUse = await _positionService.IsPositionInUse(request.PositionId);

        if (isPositionInUse)
        {
            await SendForbiddenAsync(ct);
            return;
        }

        var deleted = await _positionService.DeleteAsync(request.PositionId);

        if (!deleted)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendNoContentAsync(ct);
    }
}
