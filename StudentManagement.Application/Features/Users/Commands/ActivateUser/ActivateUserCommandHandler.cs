using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Users.Commands.ActivateUser;

public class ActivateUserCommandHandler : IRequestHandler<ActivateUserCommand, ResponseDto<UserResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public ActivateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<UserResponseDto>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync
            (x => x.Id == request.UserId, cancellationToken: cancellationToken);

        if (user == null)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }
        
        if (user.UserRole == UserRole.Student)
        {
            var student = await _context.Students.FirstOrDefaultAsync
                (x => x.UserId == user.Id, cancellationToken);
            if (student == null)
            {
                return new ResponseDto<UserResponseDto>
                {
                    Status = false, 
                    Message = "This student has not been approved yet. Use the approve endpoint",
                    Data = null
                };
            }
            student.IsActive = true;
        }
        else if (user.UserRole == UserRole.Teacher)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.UserId == user.Id, cancellationToken);
            if (teacher == null)
            {
                return new ResponseDto<UserResponseDto>
                    { 
                        Status = false, 
                        Message = "Teacher profile not found for this user", 
                        Data = null 
                    };
            }
            teacher.IsActive = true;
        }

        if (user.IsActive)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User is already active",
                Data = null
            };
        }
        
        user.IsActive = true;
        user.UpdatedAt = DateTime.UtcNow;
        
        await _context.SaveChangesAsync(cancellationToken);
        return new ResponseDto<UserResponseDto>
        {
            Status = true,
            Message = "User has been activated",
            Data = new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive,
                UpdatedAt = user.UpdatedAt,
                UserRole = user.UserRole
            }
        };
    }
}