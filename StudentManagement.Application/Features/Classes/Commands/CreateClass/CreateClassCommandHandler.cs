using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Features.Classes.Commands.CreateClass;

public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, ResponseDto<ClassResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateClassCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ResponseDto<ClassResponseDto>> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var classExists = await _dbContext.SchoolClasses.AnyAsync(
                x => x.Name == request.Request.Name && 
                     x.AcademicYear == request.Request.AcademicYear, cancellationToken);


        if (classExists)
        {
            return new ResponseDto<ClassResponseDto>
            {
                Status = false,
                Message = "Class already exists.",
                Data = null
            };
        }

        var schoolClass = new SchoolClass
        {
            AcademicYear = request.Request.AcademicYear,
            Name = request.Request.Name,
            IsActive = true

        };
        
        await _dbContext.SchoolClasses.AddAsync(schoolClass, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new ResponseDto<ClassResponseDto>
        {
            Status = true,
            Message = "Class created successfully.",
            Data = new ClassResponseDto
            {
                Id = schoolClass.Id,
                AcademicYear = schoolClass.AcademicYear,
                Name = schoolClass.Name,
                IsActive = schoolClass.IsActive
            }
        };
    }
}