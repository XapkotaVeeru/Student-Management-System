using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Users.Commands.ApproveUser;

public class ApproveUserCommandHandler : IRequestHandler<ApproveUserCommand, ResponseDto<RegisterResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public ApproveUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<RegisterResponseDto>> Handle(ApproveUserCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Request;

        var user = await _context.Users.FirstOrDefaultAsync
            (x => x.Id == request.UserId, cancellationToken);
        
        if (user == null)
        {
            return Fail("User not found");
        }
        
        if (user.UserRole != UserRole.Student)
        {
            return Fail("Only student registrations can be approved");
        }
        
        if (user.IsActive)
        {
            return Fail("User is already approved");
        }

        var hasProfile = await _context.Students.AnyAsync
            (x => x.UserId == user.Id, cancellationToken);
        
        if (hasProfile)
        {
            return Fail("This user already has a student profile. Use the activate endpoint to re-enable the account");
        }

        if (!dto.IsApproved)
        {
            return new ResponseDto<RegisterResponseDto>
            {
                Status = true,
                Message = "User was not approved. The account remains inactive",
                Data = new RegisterResponseDto
                {
                    UserId = user.Id, Email = user.Email, Username = user.Username,
                    IsActive = user.IsActive, Message = "Your registration was not approved"
                }
            };
        }

        var classExists = await _context.SchoolClasses.AnyAsync
            (x => x.Id == dto.SchoolClassId && x.IsActive, cancellationToken);
        
        if (!classExists)
        {
            return Fail("School class not found or inactive");
        }

        user.IsActive = true;
        user.UpdatedAt = DateTime.UtcNow;

        var student = new Student
        {
            UserId = user.Id,
            Email = user.Email,
            FirstName = dto.FirstName.Trim(),
            LastName = dto.LastName.Trim(),
            StudentNumber = $"STU-{user.Id:D5}",
            Gender = dto.Gender,
            AdmissionNumber = $"ADM-{user.Id:D5}",
            Address = dto.Address,
            SchoolClassId = dto.SchoolClassId,
            EnrollmentDate = DateTime.UtcNow,
            PhoneNumber = string.IsNullOrWhiteSpace(dto.PhoneNumber) ? null : dto.PhoneNumber.Trim(),
            DateOfBirth = dto.DateOfBirth,
            IsActive = true
        };

        await _context.Students.AddAsync(student, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<RegisterResponseDto>
        {
            Status = true,
            Message = "User successfully approved",
            Data = new RegisterResponseDto
            {
                UserId = user.Id, Email = user.Email, Username = user.Username,
                IsActive = user.IsActive, Message = "Your account has been approved, You can now Log in"
            }
        };
    }

    private static ResponseDto<RegisterResponseDto> Fail(string message) => new()
    {
        Status = false, 
        Message = message, 
        Data = null 
    };
}