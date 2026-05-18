using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IMapper _mapper;

    public LoginCommandHandler(IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository, IPasswordHasher hasher, 
        IRefreshTokenGenerator refreshTokenGenerator, IJwtTokenGenerator jwtGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _hasher = hasher;
        _jwtGenerator = jwtGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(LoginCommand command, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(command.Request.Email, ct);

        if (user is null)
            throw new ArgumentException("INVALID_CREDENTIALS");

        if (!_hasher.Verify(command.Request.Password, user.PasswordHash))
            throw new ArgumentException("INVALID_CREDENTIALS");
        return AuthorizationHelper.SetUpTokens(user, _refreshTokenRepository, _jwtGenerator, _refreshTokenGenerator, _mapper, ct);
    }
}