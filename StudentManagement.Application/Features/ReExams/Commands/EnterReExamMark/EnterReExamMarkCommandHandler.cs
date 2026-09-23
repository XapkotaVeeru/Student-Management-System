using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.ReExams.Commands.EnterReExamMark;

public class EnterReExamMarkCommandHandler : IRequestHandler<EnterReExamMarkCommand, ResponseDto<MarkResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public EnterReExamMarkCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<MarkResponseDto>> Handle(EnterReExamMarkCommand request, 
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
        
        var teacher = await _context.Teachers.FirstOrDefaultAsync(x => 
                    x.UserId == userId, cancellationToken);

        if (teacher == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Teacher profile not found",
                Data = null
            };
        }

        var application = await _context.ReExamApplications.FirstOrDefaultAsync(
                x => x.Id == request.ReExamApplicationId, cancellationToken);

        if (application == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Re-exam application not found",
                Data = null
            };
        }
        
        if (application.Status != ReExamStatus.Approved)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Only approved re-exam applications can receive marks",
                Data = null
            };
        }
        
        var exam = await _context.Exams.FirstOrDefaultAsync(x => 
                    x.Id == application.ExamId, cancellationToken);

        if (exam == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Exam not found",
                Data = null
            };
        }

        if (request.MarksObtained < 0)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Marks obtained cannot be negative",
                Data = null
            };
        }

        if (request.MaxMarks <= 0)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Maximum marks must be greater than zero",
                Data = null
            };
        }

        if (request.MarksObtained > request.MaxMarks)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Marks obtained cannot exceed maximum marks",
                Data = null
            };
        }

        var teacherAssignment = await _context.TeacherAssignments.FirstOrDefaultAsync(
                x =>
                    x.TeacherId == teacher.Id &&
                    x.SchoolClassId == exam.SchoolClassId &&
                    x.SubjectId == application.SubjectId,
                cancellationToken);

        if (teacherAssignment == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Teacher is not assigned to this class and subject",
                Data = null
            };
        }
        
        var existingMark = await _context.ExamMarks.FirstOrDefaultAsync(
                x =>
                    x.ExamId == application.ExamId &&
                    x.StudentId == application.StudentId &&
                    x.SubjectId == application.SubjectId,
                cancellationToken);

        if (existingMark != null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "A mark already exists for this student, exam and subject",
                Data = null
            };
        }
        var mark = new ExamMark
        {
            ExamId = application.ExamId,
            StudentId = application.StudentId,
            SubjectId = application.SubjectId,
            MarksObtained = request.MarksObtained,
            MaxMarks = request.MaxMarks,
            EnteredByTeacherId = teacher.Id,
            EnteredOn = DateTime.UtcNow,
            UpdatedOn = DateTime.UtcNow,
            Status = MarkStatus.Draft
        };

        await _context.ExamMarks.AddAsync(
            mark,
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
            Message = "Re-exam mark entered successfully",
            Data = response
        };
    }
}