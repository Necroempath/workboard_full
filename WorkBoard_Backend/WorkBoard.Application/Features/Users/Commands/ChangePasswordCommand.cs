using MediatR;

namespace WorkBoard.Application.Features.Users.Commands;

public sealed record ChangePasswordCommand(ChangePasswordRequest Request) : IRequest;
