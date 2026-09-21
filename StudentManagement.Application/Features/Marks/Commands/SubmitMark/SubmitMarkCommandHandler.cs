using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Marks.Commands.SubmitMark;

public class SubmitMarkCommandHandler
    : IRequestHandler<SubmitMarkCommand, ResponseDto<MarkResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public SubmitMarkCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<MarkResponseDto>> Handle(SubmitMarkCommand request, CancellationToken cancellationToken)
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

        var teacher = await _context.Teachers.FirstOrDefaultAsync( x => x.UserId == userId,
                cancellationToken);

        if (teacher == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Teacher profile not found",
                Data = null
            };
        }

        var mark = await _context.ExamMarks.FirstOrDefaultAsync(
                x => x.Id == request.MarkId,
                cancellationToken);

        if (mark == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Mark not found",
                Data = null
            };
        }

        if (mark.EnteredByTeacherId != teacher.Id)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "You can only submit marks entered by you",
                Data = null
            };
        }

        if (mark.Status != MarkStatus.Draft)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Only draft marks can be submitted",
                Data = null
            };
        }

        mark.Status = MarkStatus.Submitted;
        mark.UpdatedOn = DateTime.UtcNow;

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
            Message = "Mark submitted successfully",
            Data = response
        };
    }
}