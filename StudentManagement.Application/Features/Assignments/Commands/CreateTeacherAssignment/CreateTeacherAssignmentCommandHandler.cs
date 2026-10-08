using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Features.Assignments.Commands.CreateTeacherAssignment;

public class CreateTeacherAssignmentCommandHandler : IRequestHandler<CreateTeacherAssignmentCommand, 
    ResponseDto<TeacherAssignmentResponseDto>>
{
    private readonly IApplicationDbContext _context;
    public CreateTeacherAssignmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ResponseDto<TeacherAssignmentResponseDto>> Handle(CreateTeacherAssignmentCommand request, 
        CancellationToken cancellationToken)
    {
        var dto = request.Assignment;

        var teacherExists = await _context.Teachers.AnyAsync(x => x.Id == dto.TeacherId
                      && x.IsActive, cancellationToken: cancellationToken);

        if (!teacherExists)
        {
            return new ResponseDto<TeacherAssignmentResponseDto>
            {
                Data = null,
                Message = "Teacher not found",
                Status = false
            };
        }
        
        var subjectExists = await _context.Subjects.AnyAsync(
            x => x.Id == dto.SubjectId && x.IsActive,
            cancellationToken);

        if (!subjectExists)
        {
            return new ResponseDto<TeacherAssignmentResponseDto>
            {
                Data = null,
                Message = "Subject not found",
                Status = false
            };
        }

        var classExist = await _context.SchoolClasses.AnyAsync(x =>
            x.Id == dto.SchoolClassId && x.IsActive, cancellationToken: cancellationToken);

        if (!classExist)
        {
            return new ResponseDto<TeacherAssignmentResponseDto>
            {
                Data = null,
                Message = "Class not found",
                Status = false
            };
        }

        var assignmentExists = await _context.TeacherAssignments.AnyAsync(x =>
                x.TeacherId == dto.TeacherId && x.SchoolClassId == 
                dto.SchoolClassId &&
                x.SubjectId == dto.SubjectId, cancellationToken: cancellationToken);


        if (assignmentExists)
        {
            return new ResponseDto<TeacherAssignmentResponseDto>
            {
                Data = null,
                Message = "Assignment already exists",
                Status = false
            };
        }

        var assignment = new TeacherAssignment
        {
            SubjectId = dto.SubjectId,
            TeacherId = dto.TeacherId,
            SchoolClassId = dto.SchoolClassId,
        };
        
        await _context.TeacherAssignments.AddAsync(assignment, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<TeacherAssignmentResponseDto>
        {
            Data = new TeacherAssignmentResponseDto
            {
                Id = assignment.Id,
                SubjectId = assignment.SubjectId,
                TeacherId = assignment.TeacherId,
                SchoolClassId = assignment.SchoolClassId
            },
            Message = "Assignment created",
            Status = true
        };
        
    }
}