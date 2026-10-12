using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Exams;

namespace StudentManagement.Application.Features.Exams.Commands.UpdateExam;

public class UpdateExamCommand : IRequest<ResponseDto<ExamResponseDto>>
{
    public int ExamId { get; }
    public UpdateExamDto Exam { get; }

    public UpdateExamCommand(int examId, UpdateExamDto exam)
    {
        ExamId = examId;
        Exam = exam;
    }
}