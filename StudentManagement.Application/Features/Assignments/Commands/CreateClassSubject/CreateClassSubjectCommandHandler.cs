using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Features.Assignments.Commands.CreateClassSubject;

public class CreateClassSubjectCommandHandler : IRequestHandler<CreateClassSubjectCommand,
    ResponseDto<ClassSubjectResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public CreateClassSubjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<ClassSubjectResponseDto>> Handle(CreateClassSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.ClassSubject;

        var schoolClass = await _context.SchoolClasses.FirstOrDefaultAsync(
            x => x.Id == dto.SchoolClassId && x.IsActive, cancellationToken);

        if (schoolClass == null)
        {
            return new ResponseDto<ClassSubjectResponseDto>
            {
                Status = false,
                Message = "Class not found",
                Data = null
            };
        }

        var subject = await _context.Subjects.FirstOrDefaultAsync(
            x => x.Id == dto.SubjectId && x.IsActive, cancellationToken);

        if (subject == null)
        {
            return new ResponseDto<ClassSubjectResponseDto>
            {
                Status = false,
                Message = "Subject not found",
                Data = null
            };
        }

        var alreadyLinked = await _context.ClassSubjects.AnyAsync(
            x => x.SchoolClassId == dto.SchoolClassId && x.SubjectId == dto.SubjectId,
            cancellationToken);

        if (alreadyLinked)
        {
            return new ResponseDto<ClassSubjectResponseDto>
            {
                Status = false,
                Message = "Subject is already linked to this class",
                Data = null
            };
        }

        var classSubject = new ClassSubject
        {
            SchoolClassId = dto.SchoolClassId,
            SubjectId = dto.SubjectId
        };

        await _context.ClassSubjects.AddAsync(classSubject, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<ClassSubjectResponseDto>
        {
            Status = true,
            Message = "Subject linked to class",
            Data = new ClassSubjectResponseDto
            {
                Id = classSubject.Id,
                SchoolClassId = schoolClass.Id,
                SchoolClassName = schoolClass.Name,
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                SubjectCode = subject.SubjectCode
            }
        };
    }
}