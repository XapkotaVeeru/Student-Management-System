using MediatR;
using StudentManagement.Application.DTOs.Exams;

namespace StudentManagement.Application.Features.Exams.Queries.GetExams;

public class GetExamsQuery : IRequest<List<ExamResponseDto>>
{
}