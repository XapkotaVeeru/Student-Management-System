using FluentValidation;
using StudentManagement.Application.Features.Assignments.Commands.CreateTeacherAssignment;

namespace StudentManagement.Application.Features.Assignments.Validators;

public class CreateTeacherAssignmentCommandValidator : AbstractValidator<CreateTeacherAssignmentCommand>
{
    public CreateTeacherAssignmentCommandValidator()
    {
        RuleFor(x => x.Assignment).SetValidator(new CreateTeacherAssignmentValidator());
    }
}