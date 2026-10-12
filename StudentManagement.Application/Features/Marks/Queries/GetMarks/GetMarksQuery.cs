using MediatR;
using StudentManagement.Application.DTOs.Marks;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Marks.Queries.GetMarks;

public class GetMarksQuery : IRequest<List<MarkResponseDto>>
{
    public MarkStatus? Status { get; }
    public int? ExamId { get; }
    public bool? IsReExam { get; }

    public GetMarksQuery(MarkStatus? status, int? examId, bool? isReExam)
    {
        Status = status;
        ExamId = examId;
        IsReExam = isReExam;
    }
}