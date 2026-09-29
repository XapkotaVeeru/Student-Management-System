using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;

namespace StudentManagement.Application.Features.Subjects.Commands.ActivateSubject;

public class ActivateSubjectCommand : IRequest<ResponseDto<SubjectResponseDto>>
{
    public int SubjectId { get; }

    public ActivateSubjectCommand(int subjectId)
    {
        SubjectId = subjectId;
    }
}