using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Exams;

namespace StudentManagement.Application.Features.Exams.Commands.CreateExam;

public class CreateExamCommand : IRequest<ResponseDto<int>>
{
    public CreateExamDto Exam { get; }

    public CreateExamCommand(CreateExamDto exam)
    {
        Exam = exam;
    }
}