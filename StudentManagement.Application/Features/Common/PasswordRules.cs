using FluentValidation;

namespace StudentManagement.Application.Features.Common;

public static class PasswordRules
{
    public static IRuleBuilderOptions<T, string> MustBeStrongPassword<T>(
        this IRuleBuilder<T, string> rule)
    {
        return rule
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters")
            .Matches("[A-Z]").WithMessage("Password must contain an uppercase letter")
            .Matches("[a-z]").WithMessage("Password must contain a lowercase letter")
            .Matches("[0-9]").WithMessage("Password must contain a number");
    }
}