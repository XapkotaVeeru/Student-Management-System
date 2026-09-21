using MediatR;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Authentication.Commands.Login;

public class LoginCommand :IRequest<ResponseDto<LoginResponseDto>>
{
    public LoginRequestDto Request { get; set; }

    public LoginCommand(LoginRequestDto request)
    {
        Request = request;
    }
}