using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Students.Queries.GetMySubjects;

public class GetMySubjectsQueryHandler 
    : IRequestHandler<
        GetMySubjectsQuery,
        ResponseDto<IEnumerable<SubjectResponseDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetMySubjectsQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<IEnumerable<SubjectResponseDto>>> Handle(
        GetMySubjectsQuery request,
        CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId == null)
        {
            return new ResponseDto<IEnumerable<SubjectResponseDto>>
            {
                Status = false,
                Message = "User not authenticated",
                Data = null
            };
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(
                x => x.UserId == _currentUserService.UserId,
                cancellationToken);

        if (student == null)
        {
            return new ResponseDto<IEnumerable<SubjectResponseDto>>
            {
                Status = false,
                Message = "Student not found",
                Data = null
            };
        }

        var subjects = await _context.ClassSubjects
            .Where(x => x.SchoolClassId == student.SchoolClassId)
            .Join(
                _context.Subjects,
                classSubject => classSubject.SubjectId,
                subject => subject.Id,
                (classSubject, subject) => new SubjectResponseDto
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    SubjectCode = subject.SubjectCode,
                    Description = subject.Description
                })
            .ToListAsync(cancellationToken);

        return new ResponseDto<IEnumerable<SubjectResponseDto>>
        {
            Status = true,
            Message = "Subjects retrieved successfully",
            Data = subjects
        };
    }
}