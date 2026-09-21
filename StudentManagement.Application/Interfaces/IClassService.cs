using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Interfaces;

public interface IClassService
{
    Task<ResponseDto<IEnumerable<ClassResponseDto>>> GetAllAsync();

    Task<ResponseDto<ClassResponseDto>> GetByIdAsync(int id);

    Task<ResponseDto<ClassResponseDto>> CreateAsync(CreateClassDto classes);
    
    Task<ResponseDto<ClassResponseDto>> UpdateAsync(int id, UpdateClassDto classes);
    
    Task<ResponseDto<bool>> DeleteAsync(int id);

}