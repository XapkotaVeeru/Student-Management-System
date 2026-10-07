namespace StudentManagement.Application.DTOs.Teachers;

public class TeacherClassResponseDto
{
    public int ClassId { get; set; }
    public string ClassName { get; set; } = string.Empty;
    public string AcademicYear { get; set; } = string.Empty;
}