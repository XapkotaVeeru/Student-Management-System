using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.ReExam;

public class MyReExamResponseDto
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string ExamName { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public ReExamStatus Status { get; set; }
    public DateTime AppliedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string Comment { get; set; } = string.Empty;
}