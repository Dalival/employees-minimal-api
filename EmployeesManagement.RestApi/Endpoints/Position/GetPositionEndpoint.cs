using EmployeesManagement.RestApi.Mappers;
using EmployeesManagement.RestApi.Contracts.Requests.Position;
using EmployeesManagement.RestApi.Contracts.Responses;
using EmployeesManagement.RestApi.Services.Interfaces;

using FastEndpoints;

using Microsoft.AspNetCore.Authorization;

namespace EmployeesManagement.RestApi.Endpoints.Position;

[HttpGet("positions/{id}"), AllowAnonymous] //todo check do I need {id:int}
public class GetPositionEndpoint : Endpoint<GetPositionRequest, PositionResponse>
{
    private readonly IPositionService _positionService;

    public GetPositionEndpoint(IPositionService positionService)
    {
        _positionService = positionService;
    }

    public override async Task HandleAsync(GetPositionRequest request, CancellationToken ct)
    {
        if (request.PositionId <= 0)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var position = await _positionService.GetAsync(request.PositionId);

        if (position is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = position.ToPositionResponse();
        await SendOkAsync(response, ct);
    }
}
