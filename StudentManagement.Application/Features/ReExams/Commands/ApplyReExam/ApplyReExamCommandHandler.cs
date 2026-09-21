using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.ReExam;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.ReExams.Commands.ApplyReExam;

public class ApplyReExamCommandHandler
    : IRequestHandler<ApplyReExamCommand, ResponseDto<ReExamResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public ApplyReExamCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<ReExamResponseDto>> Handle(ApplyReExamCommand request, 
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "User not authenticated",
                Data = null
            };
        }

        var student = await _context.Students.FirstOrDefaultAsync(
                x => x.UserId == userId,
                cancellationToken);

        if (student == null)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "Student profile not found",
                Data = null
            };
        }

        var exam = await _context.Exams.FirstOrDefaultAsync(
                x => x.Id == request.Request.ExamId,
                cancellationToken);

        if (exam == null)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "Exam not found",
                Data = null
            };
        }

        var subject = await _context.Subjects.FirstOrDefaultAsync(
                x => x.Id == request.Request.SubjectId,
                cancellationToken);

        if (subject == null)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "Subject not found",
                Data = null
            };
        }

        if (student.SchoolClassId != exam.SchoolClassId)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "Student does not belong to the exam's class",
                Data = null
            };
        }

        var mark = await _context.ExamMarks.FirstOrDefaultAsync(
                x =>
                    x.ExamId == request.Request.ExamId &&
                    x.StudentId == student.Id &&
                    x.SubjectId == request.Request.SubjectId,
                cancellationToken);

        if (mark == null)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "No mark found for this subject",
                Data = null
            };
        }

        var existingApplication = await _context.ReExamApplications.FirstOrDefaultAsync(
                x =>
                    x.ExamId == request.Request.ExamId &&
                    x.StudentId == student.Id &&
                    x.SubjectId == request.Request.SubjectId,
                cancellationToken);

        if (existingApplication != null)
        {
            return new ResponseDto<ReExamResponseDto>
            {
                Status = false,
                Message = "Re-exam application already exists",
                Data = null
            };
        }

        var application = new ReExamApplication
        {
            ExamId = request.Request.ExamId,
            SubjectId = request.Request.SubjectId,
            StudentId = student.Id,
            Reason = request.Request.Reason,
            Status = ReExamStatus.Pending,
            AppliedAt = DateTime.UtcNow
        };

        await _context.ReExamApplications.AddAsync(
            application,
            cancellationToken);

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
            Message = "Re-exam application submitted successfully",
            Data = response
        };
    }
}