using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Exams.Commands.PublishExam;

public class PublishExamCommandHandler
    : IRequestHandler<PublishExamCommand, ResponseDto<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public PublishExamCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<bool>> Handle(PublishExamCommand request, CancellationToken cancellationToken)
    {
        var adminUserId = _currentUserService.UserId;

        if (adminUserId == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "User not authenticated",
                Data = false
            };
        }

        var exam = await _context.Exams.FirstOrDefaultAsync(
                x => x.Id == request.ExamId, cancellationToken);

        if (exam == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Exam not found",
                Data = false
            };
        }

        if (exam.IsPublished)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Exam is already published",
                Data = false
            };
        }

        var hasPendingMarks = await _context.ExamMarks.AnyAsync(
                x =>
                    x.ExamId == exam.Id &&
                    x.Status != Domain.Enums.MarkStatus.Approved,
                cancellationToken);

        if (hasPendingMarks)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Cannot publish exam while marks are not approved",
                Data = false
            };
        }

        exam.IsPublished = true;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<bool>
        {
            Status = true,
            Message = "Exam published successfully",
            Data = true
        };
    }
}