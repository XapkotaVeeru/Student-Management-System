using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly StudentManagementDbContext _dbContext;
    private readonly PasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public AuthService(StudentManagementDbContext dbContext,  
        PasswordService passwordService, IJwtService jwtService)
    {
        _dbContext = dbContext;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginRequestDto dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == dto.Username);

        if (user == null)
        {
            return new ResponseDto<LoginResponseDto>
            {
                Status =  false,
                Message = "User not found",
                Data = null
            };
        }

        if (!user.IsActive)
        {
            return new ResponseDto<LoginResponseDto>()
            {
                Status = false,
                Message = "User is not active",
                Data = null
            };
        }
        
        var passwordValid = _passwordService.VerifyHashedPassword(user.PasswordHash, dto.Password);

        if (!passwordValid)
        {
            return new ResponseDto<LoginResponseDto>()
            {
                Status = false,
                Message = "Password doesn't match",
                Data = null
            };
        }

        var token = await _jwtService.GenerateTokenAsync(user);
        return new ResponseDto<LoginResponseDto>
        {
            Status = true,
            Message = "User successfully logged in",
            Data = token
        };
    }
    public Task<ResponseDto<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<bool>> LogoutAsync(RefreshTokenRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDto<RegisterResponseDto>> RegisterAsync(RegisterRequestDto dto)
    {
        var usernameExists = await _dbContext.Users.FirstOrDefaultAsync(x => x.Username == dto.Username);
        
        if (usernameExists  != null)
        {
            return new ResponseDto<RegisterResponseDto>
            {
                Status =  false,
                Message =  "Username already exists",
                Data = null
            };
        }
        
        var emailExists = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

        if (emailExists != null)
        {
            return new ResponseDto<RegisterResponseDto>
            {
                Status = false,
                Message = "Email already exists",
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
        
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return new ResponseDto<RegisterResponseDto>
        {
           Status = true,
           Message = "User successfully registered. The account is pending admin approval",
           Data = new RegisterResponseDto
           {
               Username = user.Username,
               Email = user.Email,
               UserId = user.Id,
               IsActive = user.IsActive,
               Message = "Your Account is pending admin approval"
           }
        };
    }
}