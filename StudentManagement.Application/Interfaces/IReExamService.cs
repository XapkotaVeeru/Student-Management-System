using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Exams;
using StudentManagement.Application.DTOs.ReExam;

namespace StudentManagement.Application.Interfaces;

public interface IReExamService
{
    Task<ResponseDto<IEnumerable<ReExamResponseDto>>> GetAllAsync();
    
    Task<ResponseDto<ReExamResponseDto>> GetByIdAsync(int id);

    Task<ResponseDto<ReExamResponseDto>> CreateAsync(ApplyReExamDto exam);
    
    Task<ResponseDto<ReExamResponseDto>> ReviewAsync(int id, ReviewReExamDto exam);
}