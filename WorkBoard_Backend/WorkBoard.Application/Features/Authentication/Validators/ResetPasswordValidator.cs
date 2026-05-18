using FluentValidation;
using WorkBoard.Application.Common;

namespace WorkBoard.Application.Features.Authentication.Validators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .CustomPassword().WithMessage("Password must contain [a-z] [A-Z] digits and be at least 6 characters long");
    }
}
