using StudentManagement.Domain.Enums;

namespace StudentManagement.Domain.Entities;

public class ReExamApplication
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public int SubjectId { get; set; }
    public int StudentId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public ReExamStatus Status { get; set; }
    public DateTime AppliedAt { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public int? ReviewedByUserId { get; set; }
    public string Comment { get; set; } = string.Empty;
}