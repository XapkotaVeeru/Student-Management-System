using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;

namespace StudentManagement.Application.Interfaces;

public interface IUserService
{
    Task<ResponseDto<IEnumerable<UserResponseDto>>> GetAllAsync();
    
    Task<ResponseDto<UserResponseDto>> GetByIdAsync(int userId);
    
    Task<ResponseDto<UserResponseDto>> CreateAsync(CreateUserDto dto);
    
    Task<ResponseDto<UserResponseDto>> ApproveAsync(int id, ApproveUserDto dto);
    
    Task<ResponseDto<bool>> DeleteAsync(int id);
}