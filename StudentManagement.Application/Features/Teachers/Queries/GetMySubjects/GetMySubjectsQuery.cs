using MediatR;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Queries.GetMySubjects;

public class GetMySubjectsQuery : IRequest<List<TeacherSubjectResponseDto>>
{
}