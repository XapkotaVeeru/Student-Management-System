using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Classes.Commands.UpdateClass;

public class UpdateClassCommandHandler : IRequestHandler<UpdateClassCommand, ResponseDto<ClassResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateClassCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<ClassResponseDto>> Handle(UpdateClassCommand request, 
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

        var classExists = await _dbContext.SchoolClasses
            .AnyAsync(x => x.Id != request.ClassId &&
                           x.Name == request.Request.Name &&
                           x.AcademicYear == request.Request.AcademicYear, cancellationToken);

        if (classExists)
        {
            return new ResponseDto<ClassResponseDto>
            {
                Status = false,
                Message = "Another class with the same name and academic year already exists.",
                Data = null
            };
        }

        schoolClass.Name = request.Request.Name;
        schoolClass.AcademicYear = request.Request.AcademicYear;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<ClassResponseDto>
        {
            Status = true,
            Message = "Class updated successfully.",
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