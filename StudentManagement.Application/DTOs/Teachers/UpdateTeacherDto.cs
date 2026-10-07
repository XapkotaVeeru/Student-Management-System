using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Teachers;

public class UpdateTeacherDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
}