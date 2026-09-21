namespace StudentManagement.Application.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    string? UserName { get; }
    string? Role { get; }
}