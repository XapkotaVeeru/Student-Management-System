using FluentValidation;
using StudentManagement.Application.Features.Exams.Commands.CreateExam;

namespace StudentManagement.Application.Features.Exams.Validators;

public class CreateExamCommandValidator : AbstractValidator<CreateExamCommand>
{
    public CreateExamCommandValidator()
    {
        RuleFor(x => x.Exam.Name)
            .NotEmpty().WithMessage("Exam name is required")
            .MaximumLength(100).WithMessage("Exam name must be 100 characters or less");

        RuleFor(x => x.Exam.ExamType)
            .NotEmpty().WithMessage("Exam type is required");

        RuleFor(x => x.Exam.SchoolClassId)
            .GreaterThan(0).WithMessage("SchoolClassId must be greater than 0");

        RuleFor(x => x.Exam.EndDate)
            .GreaterThanOrEqualTo(x => x.Exam.StartDate)
            .WithMessage("End date cannot be before the start date");
        
        RuleFor(x => x.Exam.ExamType)
            .NotEmpty().WithMessage("Exam type is required")
            .MaximumLength(50).WithMessage("Exam type must be 50 characters or less");
    }
}
