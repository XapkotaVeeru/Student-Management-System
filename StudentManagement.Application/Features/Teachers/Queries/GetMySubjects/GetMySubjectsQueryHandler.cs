using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Teachers;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Teachers.Queries.GetMySubjects;

public class GetMySubjectsQueryHandler
    : IRequestHandler<GetMySubjectsQuery, List<TeacherSubjectResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetMySubjectsQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<TeacherSubjectResponseDto>> Handle(GetMySubjectsQuery request, 
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (!userId.HasValue)
        {
            return new List<TeacherSubjectResponseDto>();
        }
        
        var teacher = await _context.Teachers.FirstOrDefaultAsync(
                x => x.UserId == userId.Value, cancellationToken);

        if (teacher == null)
        {
            return new List<TeacherSubjectResponseDto>();
        }
        
        var subjects = await _context.TeacherAssignments
            .Where(x => x.TeacherId == teacher.Id)
            .Join(
                _context.Subjects,
                assignment => assignment.SubjectId,
                subject => subject.Id,
                (assignment, subject) => new TeacherSubjectResponseDto
                {
                    SubjectId = subject.Id,
                    SubjectName = subject.Name,
                    SubjectCode = subject.SubjectCode
                })
            .Distinct()
            .ToListAsync(cancellationToken);

        return subjects;
    }
}