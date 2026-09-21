using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ResponseDto<RegisterResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly PasswordService _passwordService;

    public RegisterCommandHandler(IApplicationDbContext dbContext, PasswordService passwordService)
    {
        _dbContext = dbContext;
        _passwordService = passwordService;
    }

    public async Task<ResponseDto<RegisterResponseDto>> Handle(RegisterCommand command,
        CancellationToken cancellationToken)
    {
        var dto = command.Request;

        var usernameExists = await _dbContext.Users.AnyAsync(x => 
            x.Username == dto.Username, cancellationToken: cancellationToken);

        if (usernameExists)
        {
            return new ResponseDto<RegisterResponseDto>
            {
                Status = false,
                Message = "Username already exists.",
                Data = null
            };
        }
        
        var emailExists = await _dbContext.Users.AnyAsync(x => 
            x.Email == dto.Email, cancellationToken: cancellationToken);

        if (emailExists)
        {
            return new ResponseDto<RegisterResponseDto>
            {
                Status = false,
                Message = "Email already exists.",
                Data = null
            };
        }

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = _passwordService.HashPassword(dto.Password),
            UserRole = UserRole.Student,
            IsActive = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        await _dbContext.Users.AddAsync(user, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<RegisterResponseDto>
        {
            Status = true,
            Message = "User created successfully.",
            Data = new RegisterResponseDto
            {
                Username = user.Username,
                Email = user.Email,
                IsActive = user.IsActive,
                UserId = user.Id,
                Message = "Your account has been created successfully and is pending admin approval.",
            }
        };


    }
    
    
    
}