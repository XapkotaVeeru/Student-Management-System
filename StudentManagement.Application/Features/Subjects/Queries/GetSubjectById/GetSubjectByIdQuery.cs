using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;

namespace StudentManagement.Application.Features.Subjects.Queries.GetSubjectById;

public class GetSubjectByIdQuery : IRequest<ResponseDto<SubjectResponseDto>>
{
    public int SubjectId { get; }

    public GetSubjectByIdQuery(int subjectId)
    {
        SubjectId = subjectId;
    }
}