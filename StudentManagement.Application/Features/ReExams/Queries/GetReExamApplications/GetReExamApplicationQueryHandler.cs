using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.ReExam;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.ReExams.Queries.GetReExamApplications;

public class GetReExamApplicationsQueryHandler 
    : IRequestHandler<GetReExamApplicationsQuery, List<ReExamResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetReExamApplicationsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ReExamResponseDto>> Handle(
        GetReExamApplicationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.ReExamApplications.AsQueryable();

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        if (request.ExamId.HasValue)
        {
            query = query.Where(x => x.ExamId == request.ExamId.Value);
        }

        if (request.StudentId.HasValue)
        {
            query = query.Where(x => x.StudentId == request.StudentId.Value);
        }

        return await query
            .OrderByDescending(x => x.AppliedAt)
            .Select(x => new ReExamResponseDto
            {
                Id = x.Id,
                ExamId = x.ExamId,
                SubjectId = x.SubjectId,
                StudentId = x.StudentId,
                Reason = x.Reason,
                Status = x.Status,
                AppliedAt = x.AppliedAt,
                ReviewedAt = x.ReviewedAt,
                ReviewedByUserId = x.ReviewedByUserId,
                Comment = x.Comment
            })
            .ToListAsync(cancellationToken);
    }
}