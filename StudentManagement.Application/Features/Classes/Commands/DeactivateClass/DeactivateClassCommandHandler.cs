using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Classes.Commands.DeactivateClass;

public class DeactivateClassCommandHandler : IRequestHandler<DeactivateClassCommand, ResponseDto<ClassResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeactivateClassCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<ClassResponseDto>> Handle(DeactivateClassCommand request, 
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

        if (!schoolClass.IsActive)
        {
            return new ResponseDto<ClassResponseDto>
            {
                Status = false,
                Message = "Class is already inactive.",
                Data = null
            };
        }

        schoolClass.IsActive = false;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<ClassResponseDto>
        {
            Status = true,
            Message = "Class deactivated successfully.",
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