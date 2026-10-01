using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(userId, out var id) ? id : null;
        }
    }

    public string? UserName => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

    public UserRole? UserRole
    {
        get
        {
            var role = _httpContextAccessor.HttpContext?
                .User
                .FindFirstValue(ClaimTypes.Role);

            if (Enum.TryParse<UserRole>(role, out var userRole))
            {
                return userRole;
            }

            return null;
        }
    }
    
}