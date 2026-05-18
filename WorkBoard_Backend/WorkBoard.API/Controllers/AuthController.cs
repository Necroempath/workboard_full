using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WorkBoard.API.Services;
using WorkBoard.Application.Features.Authentication;
using WorkBoard.Application.Features.Authentication.Commands;

namespace WorkBoard.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICookieService _cookieService;
    private readonly IConfiguration _config;

    public AuthController(IMediator mediator, ICookieService cookieService, IConfiguration config)
    {
        _mediator = mediator;
        _cookieService = cookieService;
        _config = config;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponseDto>> Register([FromBody]RegisterRequest request, CancellationToken token)
    {
        var authResponseDto = await _mediator.Send(new RegisterCommand(request), token);
        
        _cookieService.SetRefreshToken(Response, authResponseDto.RefreshToken);

        return Ok(authResponseDto);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponseDto>> Login([FromBody]LoginRequest request, CancellationToken token)
    {
        Console.WriteLine($"?????\n{request.Email}");
        var authResponseDto = await _mediator.Send(new LoginCommand(request), token);

        _cookieService.SetRefreshToken(Response, authResponseDto.RefreshToken);

        return Ok(authResponseDto);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<string>> Refresh(CancellationToken token)
    {
        var refreshToken = _cookieService.TryGet(Request);

        if (refreshToken is null)
            return Unauthorized();

        var tokens = await _mediator.Send(new RefreshTokenCommand(refreshToken), token);

        _cookieService.SetRefreshToken(Response, tokens.RefreshToken);

        return Ok(tokens.AccessToken);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordRequest dto, CancellationToken ct)
    {
        var baseUrl = _config.GetSection("Frontend").GetSection("BaseUrl").Value;

        if (baseUrl is null) return NoContent();

        await _mediator.Send(new ForgotPasswordCommand(dto, baseUrl), ct);

        return Ok();
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest dto, CancellationToken ct)
    {
        await _mediator.Send(new ResetPasswordCommand(dto), ct);

        return Ok();
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        _cookieService.DeleteRefreshToken(Response);

        return NoContent();
    }
}