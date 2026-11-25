using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Assignments.Queries.GetTeacherAssignments;

public class GetTeacherAssignmentsQueryHandler : IRequestHandler<GetTeacherAssignmentsQuery,
    ResponseDto<IEnumerable<TeacherAssignmentListItemDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetTeacherAssignmentsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<IEnumerable<TeacherAssignmentListItemDto>>> Handle(
        GetTeacherAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.TeacherAssignments.AsQueryable();

        if (request.TeacherId.HasValue)
        {
            query = query.Where(x => x.TeacherId == request.TeacherId.Value);
        }

        if (request.SchoolClassId.HasValue)
        {
            query = query.Where(x => x.SchoolClassId == request.SchoolClassId.Value);
        }

        var items = await query
            .Join(_context.Teachers, a => a.TeacherId, t => t.Id, (a, t) => new { a, t })
            .Join(_context.SchoolClasses, x => x.a.SchoolClassId, c => c.Id, (x, c) => new { x.a, x.t, c })
            .Join(_context.Subjects, x => x.a.SubjectId, s => s.Id, (x, s) => new { x.a, x.t, x.c, s })
            .OrderBy(x => x.c.Name)
            .ThenBy(x => x.s.Name)
            .Select(x => new TeacherAssignmentListItemDto
            {
                Id = x.a.Id,
                TeacherId = x.t.Id,
                TeacherName = (x.t.FirstName + " " + x.t.LastName).Trim(),
                EmployeeNumber = x.t.EmployeeNumber,
                SchoolClassId = x.c.Id,
                SchoolClassName = x.c.Name,
                SubjectId = x.s.Id,
                SubjectName = x.s.Name
            })
            .ToListAsync(cancellationToken);

        return new ResponseDto<IEnumerable<TeacherAssignmentListItemDto>>
        {
            Status = true,
            Message = "Teacher assignments retrieved successfully",
            Data = items
        };
    }
}