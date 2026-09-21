using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;
using StudentManagement.Application.Interfaces;
using StudentManagement.Infrastructure.Data;

namespace StudentManagement.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly StudentManagementDbContext _dbContext;
    
    public UserService(StudentManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<UserResponseDto>> ApproveAsync(int id, ApproveUserDto dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user == null)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }

        user.IsActive = dto.IsApproved;
        user.UpdatedAt = DateTime.UtcNow;
        
        await _dbContext.SaveChangesAsync();

        return new ResponseDto<UserResponseDto>
        {
            Status = true,
            Message = dto.IsApproved ? "User has been approved" : "User has been NOT approved",
            Data = new UserResponseDto()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.Username,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                UserRole = user.UserRole
            }
        };

    }
    
    public Task<ResponseDto<IEnumerable<UserResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<UserResponseDto>> GetByIdAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<UserResponseDto>> CreateAsync(CreateUserDto user)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDto<bool>> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

}