using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;

namespace StudentManagement.Application.Features.Marks.Commands.SubmitMark;

public class SubmitMarkCommand
    : IRequest<ResponseDto<MarkResponseDto>>
{
    public int MarkId { get; set; }

    public SubmitMarkCommand(int markId)
    {
        MarkId = markId;
    }
}