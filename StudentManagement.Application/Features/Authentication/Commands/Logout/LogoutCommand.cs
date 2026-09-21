using MediatR;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Authentication.Commands.Logout;

public class LogoutCommand : IRequest<ResponseDto<object>>
{
    public RefreshTokenRequestDto Request { get; set; }

    public LogoutCommand(RefreshTokenRequestDto request)
    {
        Request = request;
    }
}