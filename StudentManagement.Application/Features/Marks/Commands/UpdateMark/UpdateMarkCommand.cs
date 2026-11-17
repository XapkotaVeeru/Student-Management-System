using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;

namespace StudentManagement.Application.Features.Marks.Commands.UpdateMark;

public class UpdateMarkCommand : IRequest<ResponseDto<MarkResponseDto>>
{
    public int MarkId { get; set; }
    public UpdateMarkDto Request { get; set; }

    public UpdateMarkCommand(int markId, UpdateMarkDto request)
    {
        MarkId = markId;
        Request = request;
    }
}