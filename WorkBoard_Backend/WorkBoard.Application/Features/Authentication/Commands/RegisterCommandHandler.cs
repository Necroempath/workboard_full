using AutoMapper;
using MediatR;
using WorkBoard.Application.Abstractions.Repositories;
using WorkBoard.Application.Abstractions;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Features.Authentication.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IJwtTokenGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IUserRepository userRepository, IRoleRepository roleRepository, IRefreshTokenRepository refreshTokenRepository,
        IPasswordHasher hasher, IRefreshTokenGenerator refreshTokenGenerator, IJwtTokenGenerator jwtGenerator, IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _hasher = hasher;
        _jwtGenerator = jwtGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand command, CancellationToken ct)
    {
        var user = await _userRepository.GetByEmailAsync(command.Request.Email, ct);

        if (user is not null)
            throw new InvalidOperationException($"EMAIL_ALREADY_EXISTS");

        var hash = _hasher.Hash(command.Request.Password);

        User newUser = new(command.Request.Name, command.Request.Email, hash);

        var userRole = await _roleRepository.GetByNameAsync(Role.User, ct) 
            ?? throw new InvalidOperationException($"No role {Role.User} found in storage to make initial role assigning");

        newUser.AssignRole(userRole);

        await _userRepository.AddAsync(newUser, ct);

        return AuthorizationHelper.SetUpTokens(newUser, _refreshTokenRepository, _jwtGenerator, _refreshTokenGenerator, _mapper, ct);
    }
}
