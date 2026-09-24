using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;

namespace StudentManagement.Application.Features.ReExams.Commands.PublishReExam;

public class PublishReExamCommand : IRequest<ResponseDto<MarkResponseDto>>
{
    public int MarkId { get; }

    public PublishReExamCommand(int markId)
    {
        MarkId = markId;
    }
}