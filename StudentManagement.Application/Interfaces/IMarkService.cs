using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Marks;

namespace StudentManagement.Application.Interfaces;

public interface IMarkService
{
    Task<ResponseDto<IEnumerable<MarkResponseDto>>> GetAllAsync();  
    
    Task<ResponseDto<MarkResponseDto>> GetAsync(int id);
    
    Task<ResponseDto<MarkResponseDto>> CreateAsync(EnterMarkDto exam);
    
    Task<ResponseDto<MarkResponseDto>> UpdateAsync(int id, UpdateMarkDto exam);
    
    Task<ResponseDto<bool>> DeleteAsync(int id);
}