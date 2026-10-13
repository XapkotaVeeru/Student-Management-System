using FluentValidation;
using StudentManagement.Application.Features.Authentication.Commands.Register;
using StudentManagement.Application.Features.Common;

namespace StudentManagement.Application.Features.Authentication.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Request.Username)
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 50).WithMessage("Username must be 3 to 50 characters");

        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.Request.Password).MustBeStrongPassword();
    }
}