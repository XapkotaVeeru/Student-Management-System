using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.ReExam;

namespace StudentManagement.Application.Features.ReExams.Commands.ReviewReExam;

public class ReviewReExamCommand : IRequest<ResponseDto<ReExamResponseDto>>
{
    public int ReExamApplicationId { get; }
    public ReviewReExamDto Request { get; }

    public ReviewReExamCommand(int reExamApplicationId, ReviewReExamDto request)
    {
        ReExamApplicationId = reExamApplicationId;
        Request = request;
    }
}