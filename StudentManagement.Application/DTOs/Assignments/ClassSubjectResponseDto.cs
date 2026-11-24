namespace StudentManagement.Application.DTOs.Assignments;

public class ClassSubjectResponseDto
{
    public int Id { get; set; }
    public int SchoolClassId { get; set; }
    public string SchoolClassName { get; set; } = string.Empty;
    public int SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string SubjectCode { get; set; } = string.Empty;
}