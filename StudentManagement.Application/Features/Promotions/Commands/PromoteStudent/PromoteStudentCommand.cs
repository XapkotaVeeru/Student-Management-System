using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Promotions;

namespace StudentManagement.Application.Features.Promotions.Commands.PromoteStudent;

public class PromoteStudentCommand : IRequest<ResponseDto<PromotionResponseDto>>
{
    public PromoteStudentDto Request { get; set; }
    
    public PromoteStudentCommand(PromoteStudentDto request)
    {
        Request = request;
    }
}