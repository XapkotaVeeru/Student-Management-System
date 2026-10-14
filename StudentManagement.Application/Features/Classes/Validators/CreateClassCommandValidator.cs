using System.Data;
using FluentValidation;
using StudentManagement.Application.Features.Classes.Commands.CreateClass;

namespace StudentManagement.Application.Features.Classes.Validators;

public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty().WithMessage("Class is required.")
            .MaximumLength(50).WithMessage("Class name cannot exceed 50 characters.");
        
        RuleFor(x => x.Request.AcademicYear)
            .NotEmpty().WithMessage("Academic year is required.")
            .MaximumLength(20).WithMessage("Academic year cannot exceed 20 characters.");
    }
}