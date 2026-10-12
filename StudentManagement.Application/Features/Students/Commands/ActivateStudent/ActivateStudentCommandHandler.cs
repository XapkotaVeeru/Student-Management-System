using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Students.Commands.ActivateStudent;

public class ActivateStudentCommandHandler : IRequestHandler<ActivateStudentCommand, ResponseDto<StudentResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;

    public ActivateStudentCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<StudentResponseDto>> Handle(ActivateStudentCommand request, 
        CancellationToken cancellationToken)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == request.StudentId, 
            cancellationToken: cancellationToken);

        if (student == null)
        {
            return Fail("No student found");
        }

        if (student.IsActive)
        {
            return Fail("Student is already active");
        }

        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == student.UserId,
            cancellationToken: cancellationToken);

        if (user == null)
        {
            return Fail("Linked user account not found");
        }
        
        student.IsActive = true;
        user.IsActive = true;
        user.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<StudentResponseDto>
        {
            Status = true,
            Message = "Student has been activated",
            Data = new StudentResponseDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                PhoneNumber = student.PhoneNumber,
                IsActive = student.IsActive,
                StudentNumber = student.StudentNumber,
                AdmissionNumber = student.AdmissionNumber,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender,
                Address = student.Address,
                SchoolClassId = student.SchoolClassId,
                EnrollmentDate = student.EnrollmentDate
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