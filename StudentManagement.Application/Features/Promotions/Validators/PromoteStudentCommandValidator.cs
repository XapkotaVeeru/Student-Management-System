using FluentValidation;
using StudentManagement.Application.Features.Promotions.Commands.PromoteStudent;

namespace StudentManagement.Application.Features.Promotions.Validators;

public class PromoteStudentCommandValidator : AbstractValidator<PromoteStudentCommand>
{
    public PromoteStudentCommandValidator()
    {
        RuleFor(x => x.Request.StudentId).GreaterThan(0).WithMessage("StudentId must be greater than 0");
        RuleFor(x => x.Request.FromSchoolClassId).GreaterThan(0).WithMessage("FromSchoolClassId must be greater than 0");
        RuleFor(x => x.Request.ToSchoolClassId).GreaterThan(0).WithMessage("ToSchoolClassId must be greater than 0");

        RuleFor(x => x.Request.AcademicYear)
            .InclusiveBetween(2000, 2100).WithMessage("Academic year is not valid");
    }
}