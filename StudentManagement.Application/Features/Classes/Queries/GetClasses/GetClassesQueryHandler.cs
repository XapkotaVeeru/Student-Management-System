using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Classes.Queries.GetClasses;

public class GetClassesQueryHandler
    : IRequestHandler<GetClassesQuery, ResponseDto<IEnumerable<ClassResponseDto>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetClassesQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<IEnumerable<ClassResponseDto>>> Handle(GetClassesQuery request,
        CancellationToken cancellationToken)
    {
        var classes = await _dbContext.SchoolClasses
            .Select(x => new ClassResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                AcademicYear = x.AcademicYear,
                IsActive = x.IsActive
            })
            .ToListAsync(cancellationToken);

        return new ResponseDto<IEnumerable<ClassResponseDto>>
        {
            Status = true,
            Message = "Classes retrieved successfully.",
            Data = classes
        };
    }
}