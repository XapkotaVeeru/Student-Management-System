using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;

namespace StudentManagement.Application.Features.Subjects.Queries.GetAllSubjects;

public class GetSubjectsQuery : IRequest<ResponseDto<IEnumerable<SubjectResponseDto>>>
{
}