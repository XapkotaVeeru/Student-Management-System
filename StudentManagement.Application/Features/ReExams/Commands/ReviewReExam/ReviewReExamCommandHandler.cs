using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.ReExam;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.ReExams.Commands.ReviewReExam;

public class ReviewReExamCommandHandler : IRequestHandler<ReviewReExamCommand, ResponseDto<ReExamResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ReviewReExamCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<ReExamResponseDto>> Handle(ReviewReExamCommand request, CancellationToken cancellationToken)
    {
        var adminUserId = _currentUserService.UserId;

        if (adminUserId == null)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "User not authenticated",
                Data = null
            };
        }

        var application = await _context.ReExamApplications.FirstOrDefaultAsync(x => 
                    x.Id == request.ReExamApplicationId, cancellationToken);

        if (application == null)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "Re-exam application not found",
                Data = null
            };
        }

        if (application.Status != ReExamStatus.Pending)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "Only pending applications can be reviewed",
                Data = null
            };
        }

        if (request.Request.Status != ReExamStatus.Approved &&
            request.Request.Status != ReExamStatus.Rejected)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "Application can only be approved or rejected",
                Data = null
            };
        }

        application.Status = request.Request.Status;
        application.Comment = request.Request.Reason;
        application.ReviewedAt = DateTime.UtcNow;
        application.ReviewedByUserId = adminUserId.Value;

        await _context.SaveChangesAsync(cancellationToken);

        var response = new ReExamResponseDto
        {
            Id = application.Id,
            ExamId = application.ExamId,
            SubjectId = application.SubjectId,
            StudentId = application.StudentId,
            Reason = application.Reason,
            Status = application.Status,
            AppliedAt = application.AppliedAt,
            ReviewedAt = application.ReviewedAt,
            ReviewedByUserId = application.ReviewedByUserId,
            Comment = application.Comment
        };

        return new ResponseDto<ReExamResponseDto>
        {
            Status = true,
            Message = application.Status == ReExamStatus.Approved
                ? "Re-exam application approved successfully"
                : "Re-exam application rejected successfully",
            Data = response
        };
    }
}