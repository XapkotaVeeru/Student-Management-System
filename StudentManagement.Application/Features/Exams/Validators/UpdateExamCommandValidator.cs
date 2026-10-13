using FluentValidation;
using StudentManagement.Application.Features.Exams.Commands.UpdateExam;

namespace StudentManagement.Application.Features.Exams.Validators;

public class UpdateExamCommandValidator : AbstractValidator<UpdateExamCommand>
{
    public UpdateExamCommandValidator()
    {
        RuleFor(x => x.ExamId).GreaterThan(0).WithMessage("ExamId must be greater than 0");

        RuleFor(x => x.Exam.Name)
            .NotEmpty().WithMessage("Exam name is required")
            .MaximumLength(100).WithMessage("Exam name must be 100 characters or less");

        RuleFor(x => x.Exam.ExamType).NotEmpty().WithMessage("Exam type is required");
        RuleFor(x => x.Exam.SchoolClassId).GreaterThan(0).WithMessage("SchoolClassId must be greater than 0");

        RuleFor(x => x.Exam.EndDate)
            .GreaterThanOrEqualTo(x => x.Exam.StartDate)
            .WithMessage("End date cannot be before the start date");
    }
}