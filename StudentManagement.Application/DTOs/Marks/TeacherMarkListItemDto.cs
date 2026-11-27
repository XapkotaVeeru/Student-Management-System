using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Marks;

public class TeacherMarkListItemDto
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public string ExamName { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public decimal MarksObtained { get; set; }
    public decimal MaxMarks { get; set; }
    public MarkStatus Status { get; set; }
    public bool IsReExam { get; set; }
    public bool IsPublished { get; set; }
    public DateTime UpdatedOn { get; set; }
    public string? ReviewComment { get; set; }
}