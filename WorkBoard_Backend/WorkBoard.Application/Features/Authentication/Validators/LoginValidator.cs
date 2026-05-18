using FluentValidation;
using WorkBoard.Application.Common;

namespace WorkBoard.Application.Features.Authentication.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Invalid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .CustomPassword().WithMessage("Password must contain [a-z] [A-Z] digits and be at least 6 characters long");
    }
}
