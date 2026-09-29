using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Classes.Queries.GetClassById;

public class GetClassByIdQueryHandler
    : IRequestHandler<GetClassByIdQuery, ResponseDto<ClassResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetClassByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<ClassResponseDto>> Handle(GetClassByIdQuery request, CancellationToken cancellationToken)
    {
        var schoolClass = await _dbContext.SchoolClasses.FirstOrDefaultAsync(
                x => x.Id == request.ClassId, cancellationToken);

        if (schoolClass == null)
        {
            return new ResponseDto<ClassResponseDto>
            {
                Status = false,
                Message = "Class not found.",
                Data = null
            };
        }

        return new ResponseDto<ClassResponseDto>
        {
            Status = true,
            Message = "Class retrieved successfully.",
            Data = new ClassResponseDto
            {
                Id = schoolClass.Id,
                Name = schoolClass.Name,
                AcademicYear = schoolClass.AcademicYear,
                IsActive = schoolClass.IsActive
            }
        };
    }
}