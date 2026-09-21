using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;

namespace StudentManagement.Application.Features.Students.Queries.GetMyProfile;

public class GetMyProfileQuery : IRequest<ResponseDto<StudentResponseDto>>
{
}