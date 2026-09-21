using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Users;

public class ApproveUserDto
{
    public bool IsApproved { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int SchoolClassId { get; set; }
}