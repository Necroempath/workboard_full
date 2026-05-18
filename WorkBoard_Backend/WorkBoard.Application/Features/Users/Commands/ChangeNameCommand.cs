using MediatR;

namespace WorkBoard.Application.Features.Users.Commands;

public sealed record ChangeNameCommand(ChangeNameRequest Request) : IRequest;
