using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Promotions;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Promotions.Queries.GetPromotions;

public class GetPromotionsQueryHandler : IRequestHandler<GetPromotionsQuery, List<PromotionResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetPromotionsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<PromotionResponseDto>> Handle(GetPromotionsQuery request, 
        CancellationToken cancellationToken)
    {
        var query = _context.StudentPromotions.AsQueryable();

        if (request.StudentId.HasValue)
        {
            query = query.Where(x => x.StudentId == request.StudentId.Value);
        }

        return await query
            .OrderByDescending(x => x.PromotionDate)
            .Select(x => new PromotionResponseDto
            {
                Id = x.Id,
                StudentId = x.StudentId,
                FromSchoolClassId = x.FromSchoolClassId,
                ToSchoolClassId = x.ToSchoolClassId,
                AcademicYear = x.AcademicYear,
                PromotionDate = x.PromotionDate,
                Status = x.Status,
                ApprovedByUserId = x.ReviewedByUserId
            })
            .ToListAsync(cancellationToken);
    }
}