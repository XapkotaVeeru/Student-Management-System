using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Subjects.Queries.GetAllSubjects;

public class GetSubjectsQueryHandler : IRequestHandler<GetSubjectsQuery, ResponseDto<IEnumerable<SubjectResponseDto>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetSubjectsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<IEnumerable<SubjectResponseDto>>> Handle(GetSubjectsQuery request,
        CancellationToken cancellationToken)
    {
        var subjects = await _dbContext.Subjects
            .Select(x => new SubjectResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                SubjectCode = x.SubjectCode,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);

        return new ResponseDto<IEnumerable<SubjectResponseDto>>
        {
            Status = true,
            Message = "Subjects retrieved successfully.",
            Data = subjects
        };
    }
}