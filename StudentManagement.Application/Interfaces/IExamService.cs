using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Exams;

namespace StudentManagement.Application.Interfaces;

public interface IExamService
{
    Task<ResponseDto<IEnumerable<ExamResponseDto>>> GetAllAsync();
    
    Task<ResponseDto<ExamResponseDto>> GetByIdAsync(int id);
    
    Task<ResponseDto<ExamResponseDto>> CreateAsync(CreateExamDto exam);
    
    Task<ResponseDto<ExamResponseDto>> UpdateAsync(int id, UpdateExamDto exam);
    
    Task<ResponseDto<bool>> DeleteAsync(int id);
    
}