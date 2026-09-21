using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Users.Commands.ApproveUser;

public class ApproveUserCommandHandler :IRequestHandler<ApproveUserCommand, ResponseDto<RegisterResponseDto>>
{
    private readonly IApplicationDbContext _context;
    
    public ApproveUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<RegisterResponseDto>> Handle(ApproveUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId,
            cancellationToken: cancellationToken);

        if (user == null)
        {
            return new ResponseDto<RegisterResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }
        
        user.IsActive = true;
        user.UpdatedAt = DateTime.UtcNow;

        if (user.UserRole == UserRole.Student)
        {
            var student = new Student
            {
                UserId =  user.Id,
                Email = user.Email,
                FirstName = request.Request.FirstName,
                LastName = request.Request.LastName,
                StudentNumber = $"STU-{user.Id:D5}",
                Gender = request.Request.Gender,
                AdmissionNumber = $"ADM-{user.Id:D5}",
                Address = request.Request.Address,
                SchoolClassId = request.Request.SchoolClassId,
                EnrollmentDate = DateTime.UtcNow,
                PhoneNumber = request.Request.PhoneNumber,
                DateOfBirth =  request.Request.DateOfBirth,
                IsActive = true
                
            };
            
            await _context.Students.AddAsync(student, cancellationToken);
        }
        

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<RegisterResponseDto>
        {
            Status = true,
            Message = "User successfully approved",
            Data = new RegisterResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.Username,
                IsActive = user.IsActive,
                Message = "Your account has been approved, You can now Log in"
            }
        };

    }
}
