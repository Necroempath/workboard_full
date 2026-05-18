using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;

namespace WorkBoard.Application.Features.Users.Commands;

public sealed class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{

    private readonly ICurrentUserService _userService;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _hasher;

    public ChangePasswordCommandHandler(ICurrentUserService userService, IUserRepository userRepository, 
        IPasswordHasher hasher, IRefreshTokenRepository refreshTokenRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
        _hasher = hasher;
    }

    public async Task Handle(ChangePasswordCommand command, CancellationToken ct)
    {
        var user = await _userRepository.GetByIdAsync(_userService.UserId, ct)
            ?? throw new InvalidOperationException("USER_NOT_FOUND");

        var isMatching = _hasher.Verify(command.Request.OldPassword, user.PasswordHash);

        if (!isMatching)
            throw new InvalidOperationException("INVALID_PASSWORD");

        var newPasswordHash = _hasher.Hash(command.Request.NewPassword);

        user.UpdatePasswordHash(newPasswordHash);

        await _userRepository.SaveAsync(ct);
    }
}