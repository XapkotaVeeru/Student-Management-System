using FluentValidation;
using StudentManagement.Application.DTOs.Assignments;

namespace StudentManagement.Application.Features.Assignments.Validators;

public class CreateTeacherAssignmentValidator
    : AbstractValidator<CreateTeacherAssignmentDto>
{
    public CreateTeacherAssignmentValidator()
    {
        RuleFor(x => x.TeacherId)
            .GreaterThan(0)
            .WithMessage("TeacherId must be greater than 0");

        RuleFor(x => x.SchoolClassId)
            .GreaterThan(0)
            .WithMessage("SchoolClassId must be greater than 0");

        RuleFor(x => x.SubjectId)
            .GreaterThan(0)
            .WithMessage("SubjectId must be greater than 0");
    }
}