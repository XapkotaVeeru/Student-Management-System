using MediatR;
using StudentManagement.Application.DTOs.ReExam;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.ReExams.Queries.GetReExamApplications;

public class GetReExamApplicationsQuery : IRequest<List<ReExamResponseDto>>
{
    public ReExamStatus? Status { get; }
    public int? ExamId { get; }
    public int? StudentId { get; }

    public GetReExamApplicationsQuery(ReExamStatus? status, int? examId, int? studentId)
    {
        Status = status;
        ExamId = examId;
        StudentId = studentId;
    }
}