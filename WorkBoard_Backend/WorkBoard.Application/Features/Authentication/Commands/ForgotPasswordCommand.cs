using MediatR;

namespace WorkBoard.Application.Features.Authentication.Commands;

public sealed record ForgotPasswordCommand(ForgotPasswordRequest Request, string BaseUrl) : IRequest;

