using AutoMapper;
using WorkBoard.Application.Abstractions;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Features.Authentication.Commands;

public sealed class AuthorizationHelper
{
    public static AuthResponseDto SetUpTokens(User user, IRefreshTokenRepository refreshTokenRepository, IJwtTokenGenerator jwtTokenGenerator, 
        IRefreshTokenGenerator refreshTokenGenerator, IMapper mapper, CancellationToken ct)
    {
        (var token, var hash, var expiresAt) = refreshTokenGenerator.Generate();

        RefreshToken refreshToken = new(hash, user.Id, expiresAt);

        refreshTokenRepository.AddTokenAsync(refreshToken, ct);

        var jwt = jwtTokenGenerator.Generate(user);

        var authResponseDto = mapper.Map<AuthResponseDto>(user);

        authResponseDto.AccessToken = jwt;
        authResponseDto.RefreshToken = token;

        return authResponseDto;
    }
}
