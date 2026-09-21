using StudentManagement.Application.DTOs.Auth;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Interfaces;

public interface IJwtService
{
    Task<LoginResponseDto> GenerateTokenAsync(User user);
}