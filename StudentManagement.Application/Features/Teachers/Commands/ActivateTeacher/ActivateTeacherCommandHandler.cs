using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Teachers.Commands.ActivateTeacher;

public class ActivateTeacherCommandHandler
    : IRequestHandler<ActivateTeacherCommand, ResponseDto<TeacherResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ActivateTeacherCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<TeacherResponseDto>> Handle(
        ActivateTeacherCommand request,
        CancellationToken cancellationToken)
    {
        var teacher = await _dbContext.Teachers
            .FirstOrDefaultAsync(x => x.Id == request.TeacherId, cancellationToken);

        if (teacher == null)
            return Fail("Teacher not found.");

        if (teacher.IsActive)
            return Fail("Teacher is already active.");

        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == teacher.UserId, cancellationToken);

        if (user == null)
            return Fail("Linked user account not found.");

        teacher.IsActive = true;
        user.IsActive = true;
        user.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<TeacherResponseDto>
        {
            Status = true,
            Message = "Teacher activated successfully.",
            Data = new TeacherResponseDto
            {
                Id = teacher.Id,
                EmployeeNumber = teacher.EmployeeNumber,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                IsActive = teacher.IsActive
            }
        };
    }

    private static ResponseDto<TeacherResponseDto> Fail(string message) => new()
    {
        Status = false,
        Message = message,
        Data = null
    };
}