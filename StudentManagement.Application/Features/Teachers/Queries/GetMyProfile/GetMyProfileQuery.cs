using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Queries.GetMyProfile;

public class GetMyTeacherProfileQuery : IRequest<ResponseDto<TeacherResponseDto>>
{
}