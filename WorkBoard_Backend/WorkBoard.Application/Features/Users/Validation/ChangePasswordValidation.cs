using FluentValidation;
using WorkBoard.Application.Common;
using WorkBoard.Domain.Entities;

namespace WorkBoard.Application.Features.Users.Validation;

class ChangePasswordValidation : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordValidation()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Password is required")
            .CustomPassword().WithMessage("Password must contain [a-z] [A-Z] digits and be at least 6 characters long");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("Password is required")
            .CustomPassword().WithMessage("Password must contain [a-z] [A-Z] digits and be at least 6 characters long");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required")
            .Equal(x => x.NewPassword).WithMessage("Confirm Password must match Password");
    }
}
