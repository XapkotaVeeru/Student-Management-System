namespace StudentManagement.Application.DTOs.Subjects;

public class SubjectResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SubjectCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}