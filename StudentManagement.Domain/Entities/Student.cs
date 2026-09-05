using StudentManagement.Domain.Enums;

namespace StudentManagement.Domain.Entities;

public class Student
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string StudentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Address { get; set; } = string.Empty;
    public string AdmissionNumber { get; set; } = string.Empty;
    public int SchoolClassId { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool IsActive { get; set; }
}