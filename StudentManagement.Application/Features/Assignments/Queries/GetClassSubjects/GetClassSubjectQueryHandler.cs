using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Assignments.Queries.GetClassSubjects;

public class GetClassSubjectsQueryHandler : IRequestHandler<GetClassSubjectsQuery,
    ResponseDto<IEnumerable<ClassSubjectResponseDto>>>
{
    private readonly IApplicationDbContext _context;

    public GetClassSubjectsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<IEnumerable<ClassSubjectResponseDto>>> Handle(GetClassSubjectsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.ClassSubjects.AsQueryable();

        if (request.SchoolClassId.HasValue)
        {
            query = query.Where(x => x.SchoolClassId == request.SchoolClassId.Value);
        }

        var items = await query
            .Join(_context.SchoolClasses, cs => cs.SchoolClassId, c => c.Id, (cs, c) => new { cs, c })
            .Join(_context.Subjects, x => x.cs.SubjectId, s => s.Id, (x, s) => new { x.cs, x.c, s })
            .OrderBy(x => x.c.Name)
            .ThenBy(x => x.s.Name)
            .Select(x => new ClassSubjectResponseDto
            {
                Id = x.cs.Id,
                SchoolClassId = x.c.Id,
                SchoolClassName = x.c.Name,
                SubjectId = x.s.Id,
                SubjectName = x.s.Name,
                SubjectCode = x.s.SubjectCode
            })
            .ToListAsync(cancellationToken);

        return new ResponseDto<IEnumerable<ClassSubjectResponseDto>>
        {
            Status = true,
            Message = "Class subjects retrieved successfully",
            Data = items
        };
    }
}