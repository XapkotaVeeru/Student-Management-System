using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Students;

public class UpdateStudentDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public Gender? Gender { get; set; }
    public string? Address { get; set; }
    public int? SchoolClassId { get; set; }
    public string? AdmissionNumber { get; set; }
}