namespace StudentManagement.Application.DTOs.Teachers;

public class TeacherResponseDto
{
    public int Id { get; set; }
    public string EmployeeNumber { get; set; }  = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}