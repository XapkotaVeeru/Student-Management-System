using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.ReExams.Commands.PublishReExam;

public class PublishReExamCommandHandler
    : IRequestHandler<PublishReExamCommand, ResponseDto<MarkResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public PublishReExamCommandHandler(IApplicationDbContext context, 
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<MarkResponseDto>> Handle(PublishReExamCommand request, 
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "User not authenticated",
                Data = null
            };
        }

        var mark = await _context.ExamMarks.FirstOrDefaultAsync(
            x => x.Id == request.MarkId, cancellationToken);

        if (mark == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Mark not found",
                Data = null
            };
        }

        if (!mark.IsReExam)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Only re-exam marks can be published using this endpoint",
                Data = null
            };
        }

        if (mark.Status != MarkStatus.Approved)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Only approved re-exam marks can be published",
                Data = null
            };
        }

        if (mark.IsPublished)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Re-exam mark is already published",
                Data = null
            };
        }

        var reExamApplication = await _context.ReExamApplications
            .FirstOrDefaultAsync(
                x =>
                    x.ExamId == mark.ExamId &&
                    x.StudentId == mark.StudentId &&
                    x.SubjectId == mark.SubjectId &&
                    x.Status == ReExamStatus.Approved,
                cancellationToken);

        if (reExamApplication == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Approved re-exam application not found",
                Data = null
            };
        }

        mark.IsPublished = true;
        mark.UpdatedOn = DateTime.UtcNow;

        reExamApplication.Status = ReExamStatus.Completed;
        reExamApplication.ReviewedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        var response = new MarkResponseDto
        {
            Id = mark.Id,
            ExamId = mark.ExamId,
            StudentId = mark.StudentId,
            SubjectId = mark.SubjectId,
            MarksObtained = mark.MarksObtained,
            MaxMarks = mark.MaxMarks,
            EnteredOn = mark.EnteredOn,
            UpdatedOn = mark.UpdatedOn,
            Status = mark.Status,
            IsReExam = mark.IsReExam,
            IsPublished = mark.IsPublished
        };

        return new ResponseDto<MarkResponseDto>
        {
            Status = true,
            Message = "Re-exam mark published successfully",
            Data = response
        };
    }
}