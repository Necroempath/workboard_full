using MediatR;

namespace WorkBoard.Application.Features.Authentication.Commands;

public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshResponseDto>;
