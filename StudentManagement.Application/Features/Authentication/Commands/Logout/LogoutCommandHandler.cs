using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Authentication.Commands.Logout;

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, ResponseDto<object>>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;

    public LogoutCommandHandler(IApplicationDbContext context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }


    public async Task<ResponseDto<object>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x =>
            x.Token == request.Request.RefreshToken, cancellationToken);

        if (refreshToken == null)
        {
            return new ResponseDto<object>
            {
                Status = false,
                Message = "Invalid refresh token",
                Data = null
            };
        }

        if (refreshToken.RevokedAt != null)
        {
            return new ResponseDto<object>
            {
                Status = false,
                Message = "Refresh token has already been revoked",
                Data = null
            };
        }
        
        refreshToken.RevokedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<object>
        {
            Status = true,
            Message = "Logout successful",
            Data = null
        };
    }
}