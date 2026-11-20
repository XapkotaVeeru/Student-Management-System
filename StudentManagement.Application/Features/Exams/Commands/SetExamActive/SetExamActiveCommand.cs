using MediatR;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Exams.Commands.SetExamActive;

public class SetExamActiveCommand : IRequest<ResponseDto<bool>>
{
    public int ExamId { get; }
    public bool IsActive { get; }

    public SetExamActiveCommand(int examId, bool isActive)
    {
        ExamId = examId;
        IsActive = isActive;
    }
}