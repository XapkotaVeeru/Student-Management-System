using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;

namespace StudentManagement.Application.Features.ReExams.Commands.EnterReExamMark;

public class EnterReExamMarkCommand : IRequest<ResponseDto<MarkResponseDto>>
{
    public int ReExamApplicationId { get; }
    public decimal MarksObtained { get; }
    public decimal MaxMarks { get; }

    public EnterReExamMarkCommand(int reExamApplicationId, decimal marksObtained, decimal maxMarks)
    {
        ReExamApplicationId = reExamApplicationId;
        MarksObtained = marksObtained;
        MaxMarks = maxMarks;
    }
}