using FluentValidation;

namespace WorkBoard.Application.Features.Workspaces.Validators;

public class CreateWorkspaceValidator : AbstractValidator<CreateWorkspaceRequest>
{
    public CreateWorkspaceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Workspace name is required")
            .MinimumLength(3).WithMessage("Workspace name must be at least 3 characters long")
            .MaximumLength(100).WithMessage("Workspace name cannot exceed 100 characters");
    }
}
