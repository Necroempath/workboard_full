using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;

namespace WorkBoard.Application.Features.Authentication.Commands;

public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
{
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _hasher;

    public ResetPasswordCommandHandler(IPasswordResetTokenRepository passwordResetTokenRepository, 
        IUserRepository userRepository, IPasswordHasher hasher)
    {
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _userRepository = userRepository;
        _hasher = hasher;
    }

    public async Task Handle(ResetPasswordCommand command, CancellationToken ct)
    {
        var resetToken = await _passwordResetTokenRepository.GetByTokenAsync(command.Request.Token, ct);

        if (resetToken is null || resetToken.IsUsed || resetToken.ExpiresAt < DateTimeOffset.UtcNow)
            throw new InvalidOperationException("INVALID_TOKEN");

        resetToken.IsUsed = true;

        var user = await _userRepository.GetByIdAsync(resetToken.UserId, ct);

        if (user is null)
            throw new InvalidOperationException("INVALID_TOKEN");

        var passwordHash = _hasher.Hash(command.Request.Password);

        user.UpdatePasswordHash(passwordHash);

        await _userRepository.SaveAsync(ct);
    }
}