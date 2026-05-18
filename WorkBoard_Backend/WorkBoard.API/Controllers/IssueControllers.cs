using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.Issues;
using WorkBoard.Application.Features.Issues.Commands;
using WorkBoard.Application.Features.Issues.Queries;

namespace WorkBoard.API.Controllers;

[Route("issues")]
[ApiController]
[Authorize]
public sealed class IssueControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public IssueControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{projectId}/all")]
    public async Task<ActionResult<IEnumerable<IssueResponseDto>>> GetAll(Guid projectId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetAllIssuesQuery(projectId), token));
    }

    //[HttpGet("{issueId}")]
    //public async Task<ActionResult<IssueResponseDto>> GetById(Guid issueId, CancellationToken token)
    //{
    //    return Ok(await _mediator.Send(new GetIssueByIdQuery(issueId), token));
    //}

    [HttpPost]
    public async Task<ActionResult<IssueResponseDto>> Create([FromBody]CreateIssueRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new CreateIssueCommand(dto, dto.ColumnId), token));
    }

    [HttpPut]
    public async Task<ActionResult<IssueResponseDto>> Update([FromBody]UpdateIssueRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new UpdateIssueCommand(dto, dto.Id), token));
    }

    [HttpPatch]
    public async Task<ActionResult<IssueResponseDto>> Move([FromBody] MoveIssueRequest dto, CancellationToken token)
    {
        return Ok(await _mediator.Send(new MoveIssueCommand(dto, dto.IssueId), token));
    }

    [HttpDelete("{issueId}")]
    public async Task<ActionResult<bool>> Delete(Guid issueId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new DeleteIssueCommand(issueId), token));
    }
}