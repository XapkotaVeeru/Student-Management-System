namespace StudentManagement.Application.DTOs.ReExam;

public class TeacherReExamResponseDto
{
    public int ApplicationId { get; set; }
    public int ExamId { get; set; }
    public string ExamName { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public DateTime AppliedAt { get; set; }
}