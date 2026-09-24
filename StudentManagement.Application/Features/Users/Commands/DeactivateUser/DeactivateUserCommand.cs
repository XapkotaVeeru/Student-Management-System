using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;

namespace StudentManagement.Application.Features.Users.Commands.DeactivateUser;

public class DeactivateUserCommand : IRequest<ResponseDto<UserResponseDto>>
{
    public int UserId { get; set; }
    
    public DeactivateUserCommand(int userId)
    {
        UserId = userId;
    }
}