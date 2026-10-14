using FluentValidation;
using StudentManagement.Application.Features.ReExams.Commands.EnterReExamMark;

namespace StudentManagement.Application.Features.ReExams.Validators;

public class EnterReExamMarkCommandValidator : AbstractValidator<EnterReExamMarkCommand>
{
    public EnterReExamMarkCommandValidator()
    {
        RuleFor(x => x.ReExamApplicationId)
            .GreaterThan(0).WithMessage("ReExamApplicationId must be greater than 0");

        RuleFor(x => x.MaxMarks)
            .GreaterThan(0).WithMessage("Max marks must be greater than 0");

        RuleFor(x => x.MarksObtained)
            .GreaterThanOrEqualTo(0).WithMessage("Marks cannot be negative")
            .LessThanOrEqualTo(x => x.MaxMarks)
            .WithMessage("Marks obtained cannot exceed max marks");
    }
}