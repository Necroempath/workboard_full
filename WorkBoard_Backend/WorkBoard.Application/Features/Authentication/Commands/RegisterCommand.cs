using MediatR;

namespace WorkBoard.Application.Features.Authentication.Commands;

public record RegisterCommand(RegisterRequest Request) : IRequest<AuthResponseDto>;
