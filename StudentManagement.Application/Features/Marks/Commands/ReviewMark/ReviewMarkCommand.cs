using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;

namespace StudentManagement.Application.Features.Marks.Commands.ReviewMark;

public class ReviewMarkCommand :IRequest<ResponseDto<MarkResponseDto>>
{
    public int MarkId { get; set; }
    public ReviewMarkDto Request { get; set; }
    
    public ReviewMarkCommand(int markId, ReviewMarkDto request)
    {
        MarkId = markId;
        Request = request;
    }
}