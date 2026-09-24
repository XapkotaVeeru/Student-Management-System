using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;

namespace StudentManagement.Application.Features.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<ResponseDto<UserResponseDto>>
{
    public CreateUserDto User { get; set; }
    
    public CreateUserCommand(CreateUserDto user)
    {
        User = user;
    }
}