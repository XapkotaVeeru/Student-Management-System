using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;

namespace StudentManagement.Application.Interfaces;

public interface IStudentService
{
    Task<ResponseDto<IEnumerable<StudentResponseDto>>> GetAllAsync();

    Task<ResponseDto<StudentResponseDto>> GetByIdAsync(int id);

    Task<ResponseDto<StudentResponseDto>> CreateAsync(CreateStudentDto student);
    
    Task<ResponseDto<StudentResponseDto>> UpdateAsync(int id, UpdateStudentDto student);
    
    Task<ResponseDto<bool>> DeleteAsync(int id);
    
}