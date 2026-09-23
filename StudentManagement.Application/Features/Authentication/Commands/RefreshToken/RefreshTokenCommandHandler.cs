using MediatR;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Application.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommandHandler :IRequestHandler<RefreshTokenCommand, ResponseDto<LoginResponseDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IJwtService _jwtService;

    public RefreshTokenCommandHandler(IJwtService jwtService, IApplicationDbContext applicationDbContext)
    {
        _jwtService = jwtService;
        _applicationDbContext = applicationDbContext;
    }

    public async Task<ResponseDto<LoginResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken =
            await _applicationDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.Request.RefreshToken,
                cancellationToken);

        if (refreshToken == null)
        {
            return new ResponseDto<LoginResponseDto>
            {
                Status = false,
                Message = "Refresh token not found",
                Data = null
            };
        }

        if (refreshToken.RevokedAt != null)
        {
            return new ResponseDto<LoginResponseDto>
            {
                Status = false,
                Message = "Refresh token revoked",
                Data = null
            };
        }

        if (refreshToken.ExpiresAt <= DateTime.UtcNow)
        {
            refreshToken.RevokedAt = DateTime.UtcNow;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new ResponseDto<LoginResponseDto>
            {
                Status = false,
                Message = "Refresh token has expired",
                Data = null
            };
        }
        
        var user = await _applicationDbContext.Users.FirstOrDefaultAsync
            (x => x.Id == refreshToken.UserId,cancellationToken);

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
        
        refreshToken.RevokedAt = DateTime.UtcNow;
        
        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        
        var newToken = await _jwtService.GenerateTokenAsync(user);

        return new ResponseDto<LoginResponseDto>
        {
            Status = true,
            Message = "Refresh token has been refreshed",
            Data = newToken
        };

    }
}