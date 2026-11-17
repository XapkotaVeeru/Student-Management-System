using FluentValidation;
using StudentManagement.Application.Features.Users.Commands.ApproveUser;

namespace StudentManagement.Application.Features.Users.Validators;

public class ApproveUserCommandValidator : AbstractValidator<ApproveUserCommand>
{
    public ApproveUserCommandValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0).
            WithMessage("UserId must be greater than 0");

        When(x => x.Request.IsApproved, () =>
        {
            RuleFor(x => x.Request.FirstName)
                .NotEmpty().MaximumLength(50);
            RuleFor(x => x.Request.LastName)
                .NotEmpty().MaximumLength(50);
            RuleFor(x => x.Request.DateOfBirth)
                .GreaterThan(new DateTime(1900, 1, 1))
                .WithMessage("Date of birth is required")
                .LessThan(_ => DateTime.UtcNow)
                .WithMessage("Date of birth must be in the past");
            RuleFor(x => x.Request.Gender)
                .IsInEnum();
            RuleFor(x => x.Request.PhoneNumber)
                .MaximumLength(50);
            RuleFor(x => x.Request.Address).NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Request.SchoolClassId)
                .GreaterThan(0);
        });
    }
}