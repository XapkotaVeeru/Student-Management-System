namespace StudentManagement.Domain.Entities;

public class Exam
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ExamType { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public bool IsPublished { get; set; }
    public int SchoolClassId { get; set; }
}