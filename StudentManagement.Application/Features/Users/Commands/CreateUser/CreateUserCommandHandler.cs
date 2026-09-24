using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseDto<UserResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly PasswordService _passwordService;
    private readonly ICurrentUserService _currentUserService;

    public CreateUserCommandHandler(IApplicationDbContext context, PasswordService passwordService,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _passwordService = passwordService;
        _currentUserService = currentUserService;
    }
    
    public async Task<ResponseDto<UserResponseDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var admin = await _context.Users.FirstOrDefaultAsync
            (x => x.Id == _currentUserService.UserId, cancellationToken: cancellationToken);


        if (admin == null)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }

        if (admin.UserRole != UserRole.Admin)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User is not admin",
                Data = null
            };
        }

        var dto = request.User;

        var usernameExists = await _context.Users.AnyAsync
            (x => x.Username == dto.Username, cancellationToken: cancellationToken);

        var emailExists = await _context.Users.AnyAsync
            (x => x.Email == dto.Email, cancellationToken: cancellationToken);

        if (emailExists)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "Email already exists",
                Data = null
            };
        }

        if (usernameExists)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "Username already exists",
                Data = null
            };
        }

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PasswordHash = _passwordService.HashPassword(dto.Password),
            IsActive = true,
            UserRole = dto.UserRole
        };
        

        await _context.Users.AddAsync(user, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);
        return new ResponseDto<UserResponseDto>
        {
            Status = true,
            Message = "User created",
            Data = new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsActive = user.IsActive,
                UserRole = user.UserRole
            }
        };

    }
}