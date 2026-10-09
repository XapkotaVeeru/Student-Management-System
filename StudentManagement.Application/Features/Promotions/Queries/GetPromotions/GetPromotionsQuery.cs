using MediatR;
using StudentManagement.Application.DTOs.Promotions;

namespace StudentManagement.Application.Features.Promotions.Queries.GetPromotions;

public class GetPromotionsQuery : IRequest<List<PromotionResponseDto>>
{
    public int? StudentId { get; set; }

    public GetPromotionsQuery(int? studentId)
    {
        StudentId = studentId;
    }
}