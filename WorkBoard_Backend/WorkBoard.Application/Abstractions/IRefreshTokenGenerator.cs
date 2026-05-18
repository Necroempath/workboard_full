using WorkBoard.Domain;

namespace WorkBoard.Application.Abstractions;

public interface IRefreshTokenGenerator
{
    (string, string, DateTimeOffset) Generate();
}
