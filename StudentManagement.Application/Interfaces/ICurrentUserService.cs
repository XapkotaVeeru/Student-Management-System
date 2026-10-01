using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Interfaces;

public interface ICurrentUserService
{
    int? UserId { get; }
    string? UserName { get; }
    UserRole? UserRole { get; }
}