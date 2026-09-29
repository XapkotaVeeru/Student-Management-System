using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;

namespace StudentManagement.Application.Features.Subjects.Commands.DeactivateSubject;

public class DeactivateSubjectCommand : IRequest<ResponseDto<SubjectResponseDto>>
{
    public int SubjectId { get; }

    public DeactivateSubjectCommand(int subjectId)
    {
        SubjectId = subjectId;
    }
}