using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;

namespace WorkBoard.Application.Features.Users.Commands;

public sealed class ChangeNameCommandHandler : IRequestHandler<ChangeNameCommand>
{

    private readonly ICurrentUserService _userService;
    private readonly IUserRepository _userRepository;

    public ChangeNameCommandHandler(ICurrentUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public async Task Handle(ChangeNameCommand command, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(_userService.UserId, ct)
            ?? throw new InvalidOperationException("USER_NOT_FOUND");

        try
        {
            user.Rename(command.Request.Name);
        }
        catch (Exception)
        {
            throw new InvalidOperationException("INVALID_NAME");    
        }
        

        await _userRepository.SaveAsync(ct);
    }
}
