using EmployeesManagement.RestApi.Mappers;
using EmployeesManagement.RestApi.Contracts.Requests.Position;
using EmployeesManagement.RestApi.Contracts.Responses;
using EmployeesManagement.RestApi.Services.Interfaces;

using FastEndpoints;

using Microsoft.AspNetCore.Authorization;

namespace EmployeesManagement.RestApi.Endpoints.Position;

[HttpPut("positions"), AllowAnonymous]
public class SavePositionEndpoint : Endpoint<SavePositionRequest, PositionResponse>
{
    private readonly IPositionService _positionService;

    public SavePositionEndpoint(IPositionService positionService)
    {
        _positionService = positionService;
    }

    public override async Task HandleAsync(SavePositionRequest request, CancellationToken ct)
    {
        if (request.PositionId != default)
        {
            await UpdateExistingPosition(request, ct);
            return;
        }

        await CreateNewPosition(request, ct);
    }

    private async Task CreateNewPosition(SavePositionRequest request, CancellationToken ct)
    {
        var position = request.ToPosition();
        var newPosition = await _positionService.CreateAsync(position);

        if (newPosition is null)
        {
            //TODO not the best solution in this case...
            await SendErrorsAsync(cancellation: ct);
            return;
        }

        var response = newPosition.ToPositionResponse();
        await SendCreatedAtAsync<GetPositionEndpoint>(
            new { response.Id }, response, generateAbsoluteUrl: true, cancellation: ct);
    }

    private async Task UpdateExistingPosition(SavePositionRequest request, CancellationToken ct)
    {
        var position = request.ToPosition();
        var updatedPosition = await _positionService.UpdateAsync(position);

        if (updatedPosition is null)
        {
            // Position with provided Id was not found.
            await SendNotFoundAsync(cancellation: ct);
        }

        await SendNoContentAsync(ct);
    }
}
