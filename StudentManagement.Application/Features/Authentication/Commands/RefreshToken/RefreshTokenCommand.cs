using MediatR;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Authentication.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<ResponseDto<LoginResponseDto>>
{
    public RefreshTokenRequestDto Request { get; set; }

    public RefreshTokenCommand(RefreshTokenRequestDto request)
    {
        Request = request;
    }
}