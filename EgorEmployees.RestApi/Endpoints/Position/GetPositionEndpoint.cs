using EgorEmployees.RestApi.Contracts.Requests.Position;
using EgorEmployees.RestApi.Contracts.Responses;
using EgorEmployees.RestApi.Mappers;
using EgorEmployees.RestApi.Services.Interfaces;

using FastEndpoints;

using Microsoft.AspNetCore.Authorization;

namespace EgorEmployees.RestApi.Endpoints.Position;

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
