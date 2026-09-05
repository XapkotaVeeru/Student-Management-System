namespace StudentManagement.Application.DTOs.ReExam;

public class ApplyReExamDto
{
    public int ExamId { get; set; }
    public int SubjectId { get; set; }
    public string Reason { get; set; } = string.Empty;
}