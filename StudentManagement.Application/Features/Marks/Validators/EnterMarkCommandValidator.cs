using FluentValidation;
using StudentManagement.Application.Features.Marks.Commands.EnterMark;

namespace StudentManagement.Application.Features.Marks.Validators;

public class EnterMarkCommandValidator : AbstractValidator<EnterMarkCommand>
{
    public EnterMarkCommandValidator()
    {
        RuleFor(x => x.ExamId).
            GreaterThan(0).WithMessage("ExamId must be greater than 0");
        RuleFor(x => x.Request.StudentId).
            GreaterThan(0).WithMessage("StudentId must be greater than 0");
        RuleFor(x => x.Request.SubjectId).
            GreaterThan(0).WithMessage("SubjectId must be greater than 0");
        RuleFor(x => x.Request.MaxMarks).
            GreaterThan(0).WithMessage("Max marks must be greater than 0");

        RuleFor(x => x.Request.MarksObtained)
            .GreaterThanOrEqualTo(0).WithMessage("Marks cannot be negative")
            .LessThanOrEqualTo(x => x.Request.MaxMarks)
            .WithMessage("Marks obtained cannot exceed max marks");
    }
}