using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Teachers;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Teachers.Queries.GetMyClasses;

public class GetMyClassesQueryHandler
    : IRequestHandler<GetMyClassesQuery, List<TeacherClassResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetMyClassesQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<List<TeacherClassResponseDto>> Handle(GetMyClassesQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (!userId.HasValue)
        {
            return new List<TeacherClassResponseDto>();
        }

        var teacher = await _context.Teachers.FirstOrDefaultAsync(
                x => x.UserId == userId.Value, cancellationToken);

        if (teacher == null)
        {
            return new List<TeacherClassResponseDto>();
        }

        var classes = await _context.TeacherAssignments
            .Where(x => x.TeacherId == teacher.Id)
            .Join(
                _context.SchoolClasses,
                assignment => assignment.SchoolClassId,
                schoolClass => schoolClass.Id,
                (assignment, schoolClass) => new TeacherClassResponseDto
                {
                    ClassId = schoolClass.Id,
                    ClassName = schoolClass.Name,
                    AcademicYear = schoolClass.AcademicYear
                })
            .Distinct()
            .ToListAsync(cancellationToken);

        return classes;
    }
}