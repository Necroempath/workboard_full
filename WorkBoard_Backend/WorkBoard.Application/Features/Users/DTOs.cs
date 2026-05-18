namespace WorkBoard.Application.Features.Users;

public sealed class ChangePasswordRequest
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}

public sealed class ChangeNameRequest
{
    public string Name { get; set; } = string.Empty;
}