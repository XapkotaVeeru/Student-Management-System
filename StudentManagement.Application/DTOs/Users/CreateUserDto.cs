using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.DTOs.Users;

public class CreateUserDto
{
   public string Username { get; set; } = string.Empty;
    public string Password { get; set; }  = string.Empty;
    public string Email { get; set; }  = string.Empty;
    public UserRole UserRole { get; set; }
}