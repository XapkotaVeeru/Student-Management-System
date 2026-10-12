using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Students;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Students.Queries.GetStudents;

public class GetStudentsQueryHandler
    : IRequestHandler<GetStudentsQuery, List<StudentResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetStudentsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StudentResponseDto>> Handle(
        GetStudentsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Students.AsQueryable();

        if (request.SchoolClassId.HasValue)
        {
            query = query.Where(x => x.SchoolClassId == request.SchoolClassId.Value);
        }

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
            .Select(x => new StudentResponseDto
            {
                Id = x.Id,
                StudentNumber = x.StudentNumber,
                AdmissionNumber = x.AdmissionNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                PhoneNumber = x.PhoneNumber,
                Gender = x.Gender,
                Address = x.Address,
                SchoolClassId = x.SchoolClassId,
                EnrollmentDate = x.EnrollmentDate,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);
    }
}