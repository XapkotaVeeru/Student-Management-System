using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Marks.Queries.GetMarks;

public class GetMarksQueryHandler : IRequestHandler<GetMarksQuery, List<MarkResponseDto>>
{
    private readonly IApplicationDbContext _context;

    public GetMarksQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<MarkResponseDto>> Handle(
        GetMarksQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.ExamMarks.AsQueryable();

        if (request.Status.HasValue)
        {
            query = query.Where(x => x.Status == request.Status.Value);
        }

        if (request.ExamId.HasValue)
        {
            query = query.Where(x => x.ExamId == request.ExamId.Value);
        }

        if (request.IsReExam.HasValue)
        {
            query = query.Where(x => x.IsReExam == request.IsReExam.Value);
        }

        return await query
            .OrderByDescending(x => x.UpdatedOn)
            .Select(x => new MarkResponseDto
            {
                Id = x.Id,
                ExamId = x.ExamId,
                MarksObtained = x.MarksObtained,
                MaxMarks = x.MaxMarks,
                SubjectId = x.SubjectId,
                StudentId = x.StudentId,
                EnteredOn = x.EnteredOn,
                UpdatedOn = x.UpdatedOn,
                Status = x.Status,
                IsReExam = x.IsReExam,
                IsPublished = x.IsPublished
            })
            .ToListAsync(cancellationToken);
    }
}