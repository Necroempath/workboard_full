using MediatR;

namespace WorkBoard.Application.Features.Authentication.Commands;

public record LoginCommand(LoginRequest Request) : IRequest<AuthResponseDto>;
