using FluentValidation;
using StudentManagement.Application.Features.Common;
using StudentManagement.Application.Features.Users.Commands.CreateUser;

namespace StudentManagement.Application.Features.Users.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User.Username)
            .NotEmpty().WithMessage("Username is required")
            .Length(3, 50).WithMessage("Username must be 3 to 50 characters");

        RuleFor(x => x.User.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");

        RuleFor(x => x.User.Password).MustBeStrongPassword();

        RuleFor(x => x.User.UserRole)
            .IsInEnum().WithMessage("Invalid user role");
    }
}