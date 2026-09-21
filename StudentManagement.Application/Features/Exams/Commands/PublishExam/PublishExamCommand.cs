using MediatR;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Exams.Commands.PublishExam;

public class PublishExamCommand : IRequest<ResponseDto<bool>>
{
    public int ExamId { get; }

    public PublishExamCommand(int examId)
    {
        ExamId = examId;
    }
}