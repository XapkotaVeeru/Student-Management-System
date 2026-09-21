using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;

namespace StudentManagement.Application.Features.Marks.Commands.EnterMark;

public class EnterMarkCommand
    : IRequest<ResponseDto<MarkResponseDto>>
{
    public int ExamId { get; set; }
    public EnterMarkDto Request { get; set; }

    public EnterMarkCommand( int examId, EnterMarkDto request)
    {
        ExamId = examId;
        Request = request;
    }
}