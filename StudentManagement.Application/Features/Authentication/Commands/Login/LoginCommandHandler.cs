using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;

namespace StudentManagement.Application.Features.Authentication.Commands.Login;

public class LoginCommandHandler :IRequestHandler<LoginCommand ,ResponseDto<LoginResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly PasswordService _passwordService;
    private readonly IJwtService _jwtService;
    
    public LoginCommandHandler(IApplicationDbContext context, PasswordService passwordService, 
        IJwtService jwtService)
    {
        _context = context;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<ResponseDto<LoginResponseDto>> Handle(LoginCommand request,
        CancellationToken cancellationToken)
    {
        var dto = request.Request;

        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == dto.Username, cancellationToken: cancellationToken);

        if (user == null)
        {
            return new ResponseDto<LoginResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }

        if (!user.IsActive)
        {
            return new ResponseDto<LoginResponseDto>
            {
                Status = false,
                Message = "User is not active",
                Data = null
            };
        }

        var passwordValid = _passwordService.VerifyHashedPassword(user.PasswordHash, dto.Password);

        if (!passwordValid)
        {
            return new ResponseDto<LoginResponseDto>
            {
                Status = false,
                Message = "Invalid password",
                Data = null
            };
        }

        var token = await _jwtService.GenerateTokenAsync(user);
        return new ResponseDto<LoginResponseDto>
        {
            Status = true,
            Message = "Login successful",
            Data = token
        };
    }
}