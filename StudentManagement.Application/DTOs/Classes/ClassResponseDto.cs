namespace StudentManagement.Application.DTOs.Classes;

public class ClassResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int AcademicYear { get; set; }
}