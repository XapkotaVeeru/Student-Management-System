using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;

namespace StudentManagement.Application.Features.Students.Queries.GetMySubjects;

public class GetMySubjectsQuery 
    : IRequest<ResponseDto<IEnumerable<SubjectResponseDto>>>
{
}