using FluentValidation;
using StudentManagement.Application.Features.ReExams.Commands.ApplyReExam;

namespace StudentManagement.Application.Features.ReExams.Validators;

public class ApplyReExamCommandValidator : AbstractValidator<ApplyReExamCommand>
{
    public ApplyReExamCommandValidator()
    {
        RuleFor(x => x.Request.ExamId)
            .GreaterThan(0).WithMessage("ExamId must be greater than 0");

        RuleFor(x => x.Request.SubjectId)
            .GreaterThan(0).WithMessage("SubjectId must be greater than 0");

        RuleFor(x => x.Request.Reason)
            .NotEmpty().WithMessage("Reason is required");
    }
}