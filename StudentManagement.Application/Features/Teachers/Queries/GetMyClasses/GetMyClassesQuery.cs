using MediatR;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Queries.GetMyClasses;

public class GetMyClassesQuery : IRequest<List<TeacherClassResponseDto>>
{
}