using MediatR;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Assignments.Queries.GetClassSubjects;

public class GetClassSubjectsQuery : IRequest<ResponseDto<IEnumerable<ClassSubjectResponseDto>>>
{
    public int? SchoolClassId { get; }

    public GetClassSubjectsQuery(int? schoolClassId)
    {
        SchoolClassId = schoolClassId;
    }
}