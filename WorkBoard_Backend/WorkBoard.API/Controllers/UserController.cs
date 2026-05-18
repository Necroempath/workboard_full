using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkBoard.API.Services;
using WorkBoard.Application.Features.Users;
using WorkBoard.Application.Features.Users.Commands;

namespace WorkBoard.API.Controllers;

[Route("users")]
[ApiController]
public class UserController : Controller
{
    private readonly IMediator _mediator;
    private readonly ICookieService _cookieService;

    public UserController(IMediator mediator, ICookieService cookieService)
    {
        _mediator = mediator;
        _cookieService = cookieService;
    }

    [HttpPut("name")]
    public async Task<ActionResult> ChangeName([FromBody] ChangeNameRequest request)
    {
        await _mediator.Send(new ChangeNameCommand(request));
        
        return Ok();
    }

    [HttpPut("password")]
    public async Task<ActionResult> ChangePassword([FromBody]ChangePasswordRequest request)
    {
        await _mediator.Send(new ChangePasswordCommand(request));
        _cookieService.DeleteRefreshToken(Response);
        return Ok();
    }
}
