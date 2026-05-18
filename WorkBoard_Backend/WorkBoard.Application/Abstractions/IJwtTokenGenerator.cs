using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Abstractions;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}
