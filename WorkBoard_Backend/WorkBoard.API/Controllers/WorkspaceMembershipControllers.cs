using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.Application.Features.WorkspaceMemberships;
using WorkBoard.Application.Features.WorkspaceMemberships.Commands;
using WorkBoard.Application.Features.WorkspaceMemberships.Queries;

namespace WorkBoard.API.Controllers;

[Route("memberships")]
[ApiController]
[Authorize]
public sealed class WorkspaceMembershipControllers : ControllerBase
{
    private readonly IMediator _mediator;

    public WorkspaceMembershipControllers(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{workspaceId}")]
    public async Task<ActionResult<IEnumerable<WorkspaceMembershipResponseDto>>> GetAllMembers(Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new GetAllMembersQuery(workspaceId), token));
    }

    [HttpPost("{workspaceId}")]
    public async Task<ActionResult<WorkspaceMembershipResponseDto>> AddMember([FromBody]AddMemberRequest dto, Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new AddMemberCommand(dto, workspaceId), token));
    }

    [HttpDelete("{workspaceId}/{userId}")]
    public async Task<ActionResult<bool>> RemoveMember(Guid userId, Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new RemoveMemberCommand(userId, workspaceId), token));
    }

    [HttpPut("{workspaceId}/role")]
    public async Task<ActionResult<bool>> ChangeRole(ChangeRoleRequest dto, Guid workspaceId, CancellationToken token)
    {
        return Ok(await _mediator.Send(new ChangeRoleCommand(dto, workspaceId), token));
    }
}