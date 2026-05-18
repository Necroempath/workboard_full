using FluentValidation;
using WorkBoard.Application.Common;

namespace WorkBoard.Application.Features.Authentication.Validators;

public class RegisterValidator : AbstractValidator<RegisterRequest>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .CustomPassword().WithMessage("Password must contain [a-z] [A-Z] digits and be at least 6 characters long");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required")
            .Equal(x => x.Password).WithMessage("Confirm Password must match Password");
    }
}
    