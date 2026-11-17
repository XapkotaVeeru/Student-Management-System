using FluentValidation;
using StudentManagement.Application.Features.Marks.Commands.UpdateMark;

namespace StudentManagement.Application.Features.Marks.Validators;

public class UpdateMarkCommandValidator : AbstractValidator<UpdateMarkCommand>
{
    public UpdateMarkCommandValidator()
    {
        RuleFor(x => x.MarkId).GreaterThan(0)
            .WithMessage("MarkId must be greater than 0");
        RuleFor(x => x.Request.MaxMarks).GreaterThan(0)
            .WithMessage("Max marks must be greater than 0");
        RuleFor(x => x.Request.MarksObtained)
            .GreaterThanOrEqualTo(0).WithMessage("Marks cannot be negative")
            .LessThanOrEqualTo(x => x.Request.MaxMarks)
            .WithMessage("Marks obtained cannot exceed max marks");
    }
}