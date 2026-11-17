using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Features.Marks.Queries.GetMarks;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Marks.Commands.UpdateMark;

public class UpdateMarkCommandHandler : IRequestHandler<UpdateMarkCommand, ResponseDto<MarkResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public UpdateMarkCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<ResponseDto<MarkResponseDto>> Handle(UpdateMarkCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;

        if (userId == null)
        {
            return Fail("User not found");
        }
        
        var teacher = await _context.Teachers.FirstOrDefaultAsync
            (x => x.UserId == userId, cancellationToken: cancellationToken);
        if (teacher == null)
        {
            return Fail("Teacher not found");
        }

        if (!teacher.IsActive)
        {
            return Fail("Teacher is not active");
        }
        
        var mark = await _context.ExamMarks.FirstOrDefaultAsync
            (x => x.Id == request.MarkId, cancellationToken);

        if (mark == null)
        {
            return Fail("Mark not found");
        }

        if (mark.EnteredByTeacherId != teacher.Id)
        {
            return Fail("You cannot update this mark");
        }
        
        if (mark.Status != MarkStatus.Draft && mark.Status != MarkStatus.Rejected)
        {
            return Fail("You cannot update this mark");
        }
        
        var exam = await _context.Exams.FirstOrDefaultAsync
            (x => x.Id == mark.ExamId, cancellationToken);

        if (exam == null)
        {
            return Fail("Exam not found");
        }
        
        var stillAssigned = await _context.TeacherAssignments.AnyAsync
            (x => x.TeacherId == teacher.Id && x.SchoolClassId == exam.SchoolClassId 
                                            && x.SubjectId == mark.SubjectId , cancellationToken);

       
        if (!stillAssigned)
        {
            return Fail("You cannot update this mark");
        }
        
        mark.Status = MarkStatus.Draft;
        mark.MarksObtained =  request.Request.MarksObtained;
        mark.MaxMarks = request.Request.MaxMarks;
        mark.UpdatedOn =  DateTime.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<MarkResponseDto>
        {
            Status = true,
            Message = "Mark updated successfully",
            Data = new MarkResponseDto
            {
                Id = mark.Id,
                ExamId = mark.ExamId,
                MarksObtained = mark.MarksObtained,
                MaxMarks = mark.MaxMarks,
                SubjectId = mark.SubjectId,
                StudentId = mark.StudentId,
                EnteredOn = mark.EnteredOn,
                UpdatedOn = mark.UpdatedOn,
                Status = mark.Status,
                IsReExam = mark.IsReExam,
                IsPublished = mark.IsPublished
            }
        };

    }

    private static ResponseDto<MarkResponseDto> Fail(string message)
    {
        return new ResponseDto<MarkResponseDto>
        {
            Status = false,
            Message = message,
            Data = null
        };
    }
}