using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Interfaces;

public interface ITeacherService
{
    Task<ResponseDto<IEnumerable<TeacherResponseDto>>> GetAllAsync();
    
    Task<ResponseDto<TeacherResponseDto>> GetByIdAsync(int id);
    
    Task<ResponseDto<TeacherResponseDto>> CreateAsync(CreateTeacherDto teacher);
    
    Task<ResponseDto<TeacherResponseDto>> UpdateAsync(int id, UpdateTeacherDto teacher);
    
    Task<ResponseDto<bool>> DeleteAsync(int id);
}