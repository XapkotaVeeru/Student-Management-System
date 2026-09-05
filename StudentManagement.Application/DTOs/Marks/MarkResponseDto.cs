using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Marks;

public class MarkResponseDto
{
    public int Id { get; set; }
    public int ExamId { get; set; }
    public decimal MarksObtained { get; set; }
    public decimal MaxMarks { get; set; }
    public int SubjectId { get; set; }
    public int StudentId { get; set; }
    public DateTime EnteredOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public MarkStatus Status { get; set; }
}