using FluentValidation;
using StudentManagement.Application.Features.Subjects.Commands.CreateSubject;

namespace StudentManagement.Application.Features.Subjects.Validators;

public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Subject name is required")
            .MaximumLength(100).WithMessage("Subject name must be 100 characters or less");

        RuleFor(x => x.Request.SubjectCode)
            .NotEmpty().WithMessage("Subject code is required")
            .MaximumLength(20).WithMessage("Subject code must be 20 characters or less");

        RuleFor(x => x.Request.Description)
            .NotNull().WithMessage("Description cannot be null")
            .MaximumLength(500).WithMessage("Description must be 500 characters or less");
    }
}