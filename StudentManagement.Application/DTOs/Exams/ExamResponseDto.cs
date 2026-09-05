namespace StudentManagement.Application.DTOs.Exams;

public class ExamResponseDto
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int SchoolClassId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ExamType { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public bool IsPublished { get; set; }
}