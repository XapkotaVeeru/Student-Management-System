using MediatR;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Assignments.Commands.DeleteClassSubject;

public class DeleteClassSubjectCommand : IRequest<ResponseDto<bool>>
{
    public int ClassSubjectId { get; }

    public DeleteClassSubjectCommand(int classSubjectId)
    {
        ClassSubjectId = classSubjectId;
    }
}