namespace WorkBoard.Application.Abstractions;

public interface IPasswordResetTokenGenerator
{
    (string, string, DateTimeOffset) Generate();
}
