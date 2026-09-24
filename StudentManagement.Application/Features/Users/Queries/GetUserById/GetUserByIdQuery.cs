using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;

namespace StudentManagement.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQuery :IRequest<ResponseDto<UserResponseDto>>
{
    public int Id { get; set; }
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}