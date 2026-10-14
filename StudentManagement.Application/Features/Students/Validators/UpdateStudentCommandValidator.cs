using FluentValidation;
using StudentManagement.Application.Features.Students.Commands.UpdateStudent;

namespace StudentManagement.Application.Features.Students.Validators;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("StudentId must be greater than 0");

        RuleFor(x => x.Student.FirstName)
            .NotEmpty().WithMessage("First name cannot be blank")
            .When(x => x.Student.FirstName != null);

        RuleFor(x => x.Student.LastName)
            .NotEmpty().WithMessage("Last name cannot be blank")
            .When(x => x.Student.LastName != null);

        RuleFor(x => x.Student.Email)
            .EmailAddress().WithMessage("Email is not valid")
            .When(x => !string.IsNullOrEmpty(x.Student.Email));

        RuleFor(x => x.Student.DateOfBirth)
            .LessThan(_ => DateTime.UtcNow).WithMessage("Date of birth must be in the past")
            .When(x => x.Student.DateOfBirth.HasValue);

        RuleFor(x => x.Student.Gender)
            .IsInEnum().WithMessage("Gender is not valid");

        RuleFor(x => x.Student.SchoolClassId)
            .GreaterThan(0).WithMessage("SchoolClassId must be greater than 0")
            .When(x => x.Student.SchoolClassId.HasValue);
    }
}