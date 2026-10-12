using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Students.Commands.DeactivateStudent;

public class DeactivateStudentCommandHandler : IRequestHandler<DeactivateStudentCommand, 
    ResponseDto<StudentResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public DeactivateStudentCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<StudentResponseDto>> Handle(DeactivateStudentCommand request,
        CancellationToken cancellationToken)
    {
        var student = await _dbContext.Students
            .FirstOrDefaultAsync(x => x.Id == request.StudentId, cancellationToken);

        if (student == null)
        {
            return Fail("Student not found.");
        }

        if (!student.IsActive)
        {
            return Fail("Student is already inactive.");
        }
        
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.Id == student.UserId, cancellationToken);

        if (user == null)
        {
            return Fail("Linked user account not found.");
        }


        student.IsActive = false;
        user.IsActive = false;
        user.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);   // both saved together

        return new ResponseDto<StudentResponseDto>
        {
            Status = true,
            Message = "Student deactivated successfully.",
            Data = new StudentResponseDto
            {
                Id = student.Id,
                StudentNumber = student.StudentNumber,
                AdmissionNumber = student.AdmissionNumber,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                PhoneNumber = student.PhoneNumber,
                Gender = student.Gender,
                Address = student.Address,
                SchoolClassId = student.SchoolClassId,
                EnrollmentDate = student.EnrollmentDate,
                IsActive = student.IsActive
            }
        };
    }

    private static ResponseDto<StudentResponseDto> Fail(string message) => new()
    {
        Status = false,
        Message = message,
        Data = null
    };
}