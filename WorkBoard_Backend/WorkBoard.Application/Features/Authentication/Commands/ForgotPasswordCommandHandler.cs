using MediatR;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Features.Authentication.Commands;

public sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
{
    private readonly IPasswordResetTokenGenerator _generator;
    private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(IPasswordResetTokenGenerator generator, IPasswordResetTokenRepository passwordResetTokenRepository,
        IUserRepository userRepository, IEmailService emailService)
    {
        _generator = generator;
        _passwordResetTokenRepository = passwordResetTokenRepository;
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task Handle(ForgotPasswordCommand command, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(command.Request.Email, ct);

        if (user is null) return;

        var (token, hash, expiresAt) = _generator.Generate();

        PasswordResetToken passwordResetToken = new(hash, expiresAt, user.Id);

        var passwordResetTokens = await _passwordResetTokenRepository.GetByUserIdAsync(user.Id, ct);

        foreach (var prt in passwordResetTokens)
        {
            prt.IsUsed = true;
        }

        await _passwordResetTokenRepository.AddTokenAsync(passwordResetToken, ct);

        var baseUrl = command.BaseUrl;

        await _emailService.SendPasswordResetEmailAsync(user.Email, user.Name, $"{baseUrl}/reset-password?token={Uri.EscapeDataString(token)}");
    }
}