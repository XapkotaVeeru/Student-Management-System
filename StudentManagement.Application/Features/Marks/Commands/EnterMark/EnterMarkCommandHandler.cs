using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Marks.Commands.EnterMark;

public class EnterMarkCommandHandler
    : IRequestHandler<EnterMarkCommand, ResponseDto<MarkResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public EnterMarkCommandHandler( IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<MarkResponseDto>> Handle(EnterMarkCommand request, CancellationToken cancellationToken)
    {
        var teacherUserId = _currentUserService.UserId;

        if (teacherUserId == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "User not authenticated",
                Data = null
            };
        }
        
        var teacher = await _context.Teachers
            .FirstOrDefaultAsync(
                x => x.UserId == teacherUserId,
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
        
        var exam = await _context.Exams
            .FirstOrDefaultAsync(
                x => x.Id == request.ExamId,
                cancellationToken);

        if (exam == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Exam not found",
                Data = null
            };
        }
        
        if (exam.IsPublished)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Cannot enter marks for a published exam",
                Data = null
            };
        }
        
        var student = await _context.Students
            .FirstOrDefaultAsync(
                x => x.Id == request.Request.StudentId,
                cancellationToken);

        if (student == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Student not found",
                Data = null
            };
        }
        
        if (student.SchoolClassId != exam.SchoolClassId)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Student does not belong to the exam's class",
                Data = null
            };
        }
        
        var subject = await _context.Subjects
            .FirstOrDefaultAsync(
                x => x.Id == request.Request.SubjectId,
                cancellationToken);

        if (subject == null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Subject not found",
                Data = null
            };
        }
        
        var teacherAssignment = await _context.TeacherAssignments
            .FirstOrDefaultAsync(
                x =>
                    x.TeacherId == teacher.Id &&
                    x.SchoolClassId == exam.SchoolClassId &&
                    x.SubjectId == request.Request.SubjectId,
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
        
        var existingMark = await _context.ExamMarks
            .FirstOrDefaultAsync(
                x =>
                    x.ExamId == request.ExamId &&
                    x.StudentId == request.Request.StudentId &&
                    x.SubjectId == request.Request.SubjectId,
                cancellationToken);

        if (existingMark != null)
        {
            return new ResponseDto<MarkResponseDto>
            {
                Status = false,
                Message = "Mark already exists for this student and subject",
                Data = null
            };
        }
        
        var now = DateTime.UtcNow;

        var mark = new ExamMark
        {
            ExamId = request.ExamId,
            StudentId = request.Request.StudentId,
            SubjectId = request.Request.SubjectId,
            MarksObtained = request.Request.MarksObtained,
            MaxMarks = request.Request.MaxMarks,
            
            EnteredByTeacherId = teacher.Id,

            EnteredOn = now,
            UpdatedOn = now,
            Status = MarkStatus.Draft,
            IsReExam = false
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
            Status = mark.Status,
            IsReExam = mark.IsReExam,
            IsPublished = mark.IsPublished
        };
        
        return new ResponseDto<MarkResponseDto>
        {
            Status = true,
            Message = "Mark entered successfully",
            Data = response
        };
    }
}
