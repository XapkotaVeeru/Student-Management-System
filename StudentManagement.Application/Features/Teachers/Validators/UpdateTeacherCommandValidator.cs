using FluentValidation;
using StudentManagement.Application.Features.Teachers.Commands.UpdateTeacher;

namespace StudentManagement.Application.Features.Teachers.Validators;

public class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
{
    public UpdateTeacherCommandValidator()
    {
        RuleFor(x => x.TeacherId)
            .GreaterThan(0).WithMessage("TeacherId must be greater than 0");

        RuleFor(x => x.Teacher.FirstName)
            .NotEmpty().WithMessage("First name cannot be blank")
            .When(x => x.Teacher.FirstName != null);

        RuleFor(x => x.Teacher.LastName)
            .NotEmpty().WithMessage("Last name cannot be blank")
            .When(x => x.Teacher.LastName != null);

        RuleFor(x => x.Teacher.Email)
            .EmailAddress().WithMessage("Email is not valid")
            .When(x => !string.IsNullOrEmpty(x.Teacher.Email));

        RuleFor(x => x.Teacher.Gender)
            .IsInEnum().WithMessage("Gender is not valid");
    }
}