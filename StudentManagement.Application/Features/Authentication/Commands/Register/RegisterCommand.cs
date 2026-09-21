using MediatR;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Authentication.Commands.Register;

public class RegisterCommand :IRequest<ResponseDto<RegisterResponseDto>>
{
    public RegisterRequestDto Request { get; set; }

    public RegisterCommand(RegisterRequestDto request)
    {
        Request = request;
    }
}