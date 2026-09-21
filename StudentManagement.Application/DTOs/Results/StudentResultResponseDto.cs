using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Results;

public class StudentResultResponseDto
{
    public int ExamId { get; set; }
    public string ExamName { get; set; } = string.Empty;

    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;

    public decimal MarksObtained { get; set; }
    public decimal MaxMarks { get; set; }

    public MarkStatus Status { get; set; }
}