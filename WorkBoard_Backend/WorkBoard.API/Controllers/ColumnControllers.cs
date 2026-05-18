using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Workspaces;
using WorkBoard.Application.Features.Columns;
using WorkBoard.Application.Features.Columns.Commands;

namespace WorkBoard.API.Controllers;

[Route("columns")]
[ApiController]
[Authorize]
public sealed class ColumnControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public ColumnControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ColumnResponseDto>> Add([FromBody] CreateColumnRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new AddColumnCommand(dto.Name, dto.ProjectId), token));
    }

    [HttpPut]
    public async Task<ActionResult<ColumnResponseDto>> Update([FromBody] UpdateColumnRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new UpdateColumnCommand(dto.Name, dto.Id), token));
    }

    [HttpDelete("{columnId}")]
    public async Task<ActionResult<ColumnResponseDto>> Delete(Guid columnId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new DeleteColumnCommand(columnId), token));
    }
}