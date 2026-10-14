using FluentValidation;
using StudentManagement.Application.Features.Marks.Commands.ReviewMark;

namespace StudentManagement.Application.Features.Marks.Validators;

public class ReviewMarkCommandValidator : AbstractValidator<ReviewMarkCommand>
{
    public ReviewMarkCommandValidator()
    {
        RuleFor(x => x.MarkId)
            .GreaterThan(0).WithMessage("MarkId must be greater than 0");

        RuleFor(x => x.Request.Comment)
            .NotEmpty()
            .When(x => !x.Request.IsApproved)
            .WithMessage("A comment is required when rejecting a mark");
    }
}