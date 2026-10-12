using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Teachers;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Teachers.Queries.GetTeachers;

public class GetTeachersQueryHandler
    : IRequestHandler<GetTeachersQuery, List<TeacherResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTeachersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TeacherResponseDto>> Handle(
        GetTeachersQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Teachers.AsQueryable();

        if (request.IsActive.HasValue)
        {
            query = query.Where(x => x.IsActive == request.IsActive.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            var term = request.Search.Trim().ToLower();

            query = query.Where(x =>
                x.FirstName.ToLower().Contains(term) ||
                x.LastName.ToLower().Contains(term));
        }

        return await query
            .OrderBy(x => x.FirstName)
            .ThenBy(x => x.LastName)
            .Select(x => new TeacherResponseDto
            {
                Id = x.Id,
                EmployeeNumber = x.EmployeeNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}