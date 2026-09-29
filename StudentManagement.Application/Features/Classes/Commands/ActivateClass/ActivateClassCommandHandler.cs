using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Classes.Commands.ActivateClass;

public class ActivateClassCommandHandler : IRequestHandler<ActivateClassCommand, ResponseDto<ClassResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ActivateClassCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<ClassResponseDto>> Handle(ActivateClassCommand request, 
        CancellationToken cancellationToken)
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

        if (schoolClass.IsActive)
        {
            return new ResponseDto<ClassResponseDto>
            {
                Status = false,
                Message = "Class is already active.",
                Data = null
            };
        }

        schoolClass.IsActive = true;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<ClassResponseDto>
        {
            Status = true,
            Message = "Class activated successfully.",
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