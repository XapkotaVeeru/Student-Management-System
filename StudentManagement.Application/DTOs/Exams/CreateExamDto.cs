namespace StudentManagement.Application.DTOs.Exams;

public class CreateExamDto
{
    public string Name { get; set; } = string.Empty;
    public string ExamType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SchoolClassId { get; set; }
}