using MediatR;

namespace WorkBoard.Application.Features.Authentication.Commands;

public sealed record ResetPasswordCommand(ResetPasswordRequest Request) : IRequest;


