using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Teachers;

public class CreateTeacherDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}