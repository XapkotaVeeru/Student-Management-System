using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Marks.Commands.ReviewMark;

public class ReviewMarkCommandHandler
    : IRequestHandler<ReviewMarkCommand, ResponseDto<MarkResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ReviewMarkCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<MarkResponseDto>> Handle(ReviewMarkCommand request, CancellationToken cancellationToken)
    {
        var adminUserId = _currentUserService.UserId;

        if (adminUserId == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "User not authenticated",
                Data = null
            };
        }

        var mark = await _context.ExamMarks.FirstOrDefaultAsync(x => 
            x.Id == request.MarkId, cancellationToken);

        if (mark == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Mark not found",
                Data = null
            };
        }

        if (mark.Status != MarkStatus.Submitted)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Only submitted marks can be reviewed",
                Data = null
            };
        }

        mark.Status = request.Request.IsApproved
            ? MarkStatus.Approved
            : MarkStatus.Rejected;

        mark.UpdatedOn = DateTime.UtcNow;

        var approval = new MarkApproval
        {
            ExamMarkId = mark.Id,
            ApprovedByUserId = adminUserId.Value,
            IsApproved = request.Request.IsApproved,
            Comment = request.Request.Comment,
            ActionDate = DateTime.UtcNow
        };

        await _context.MarkApprovals.AddAsync(
            approval,
            cancellationToken);

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
            Status = mark.Status
        };

        return new ResponseDto<MarkResponseDto>
        {
            Status = true,
            Message = request.Request.IsApproved
                ? "Mark approved successfully"
                : "Mark rejected successfully",
            Data = response
        };
    }
}