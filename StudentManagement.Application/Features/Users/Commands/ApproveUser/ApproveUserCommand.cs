using MediatR;
using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;

namespace StudentManagement.Application.Features.Users.Commands.ApproveUser;

public class ApproveUserCommand : IRequest<ResponseDto<RegisterResponseDto>>
{
    public int UserId { get; set; }
    public ApproveUserDto Request { get; set; }

    public ApproveUserCommand(int userId, ApproveUserDto request)
    {
        UserId = userId;
        Request = request;
    }
}