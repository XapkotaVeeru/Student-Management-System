using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;

namespace StudentManagement.Application.Features.Users.Queries.GetUsers;

public class GetUsersQuery : IRequest<ResponseDto<IEnumerable<UserResponseDto>>>
{
}