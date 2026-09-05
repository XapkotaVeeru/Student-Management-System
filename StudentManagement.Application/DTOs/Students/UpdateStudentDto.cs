using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Students;

public class UpdateStudentDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public int SchoolClassId { get; set; }
}