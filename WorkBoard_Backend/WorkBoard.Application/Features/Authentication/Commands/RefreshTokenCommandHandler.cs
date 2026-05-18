using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;

    public RefreshTokenCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository,
        IJwtTokenGenerator jwtGenerator, IRefreshTokenGenerator refreshTokenGenerator)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtGenerator = jwtGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
    }

    public async Task<RefreshResponseDto> Handle(RefreshTokenCommand command, CancellationToken ct)
    {
        var refreshToken = await _refreshTokenRepository.GetByTokenAsync(command.RefreshToken, ct);

        if (refreshToken is null || refreshToken.RevokedAt != null || refreshToken.ReplacedByTokenId != null)
            throw new InvalidOperationException("Invalid Refresh Token");

        var user = await _userRepository.GetByIdAsync(refreshToken.UserId, ct);

        if (user is null)
            throw new InvalidOperationException("Invalid Refresh Token");

        (var token, var hash, var expiresAt) = _refreshTokenGenerator.Generate();

        RefreshToken newRefreshToken = new(hash, user.Id, expiresAt);

        await _refreshTokenRepository.AddTokenAsync(newRefreshToken, ct);

        var jwt = _jwtGenerator.Generate(user);

        refreshToken.Revoke();
        refreshToken.SetReplaceToken(newRefreshToken.Id);

       await _refreshTokenRepository.Save(ct);

        return new()
        {
            RefreshToken = token,
            AccessToken = jwt
        };
    }
}
