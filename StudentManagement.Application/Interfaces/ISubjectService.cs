using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Subjects;

namespace StudentManagement.Application.Interfaces;

public interface ISubjectService
{
    Task<ResponseDto<IEnumerable<SubjectResponseDto>>> GetAllAsync();

    Task<ResponseDto<SubjectResponseDto>> GetByIdAsync(int id);
    
    Task<ResponseDto<SubjectResponseDto>> CreateAsync(CreateSubjectDto subject);
    
    Task<ResponseDto<SubjectResponseDto>> UpdateAsync(int id, UpdateSubjectDto subject);
    
    Task<ResponseDto<bool>> DeleteAsync(int id);
}