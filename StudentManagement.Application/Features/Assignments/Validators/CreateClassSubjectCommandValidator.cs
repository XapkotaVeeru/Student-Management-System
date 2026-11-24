using FluentValidation;
using StudentManagement.Application.Features.Assignments.Commands.CreateClassSubject;

namespace StudentManagement.Application.Features.Assignments.Validators;

public class CreateClassSubjectCommandValidator : AbstractValidator<CreateClassSubjectCommand>
{
    public CreateClassSubjectCommandValidator()
    {
        RuleFor(x => x.ClassSubject.SchoolClassId)
            .GreaterThan(0)
            .WithMessage("SchoolClassId must be greater than 0");

        RuleFor(x => x.ClassSubject.SubjectId)
            .GreaterThan(0)
            .WithMessage("SubjectId must be greater than 0");
    }
}