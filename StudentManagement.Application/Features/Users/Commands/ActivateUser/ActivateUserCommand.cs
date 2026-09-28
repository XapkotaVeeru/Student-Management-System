using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;

namespace StudentManagement.Application.Features.Users.Commands.ActivateUser;

public class ActivateUserCommand : IRequest<ResponseDto<UserResponseDto>>
{
    public int UserId { get; }
    public ActivateUserCommand(int userId)
    {
        UserId = userId;
    }
}