using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Interfaces;

public interface IAssignmentService
{
    Task<ResponseDto<IEnumerable<ClassSubjectResponseDto>>> GetAllClassSubjectsAsync();
    
    Task<ResponseDto<IEnumerable<TeacherAssignmentResponseDto>>> GetAllTeacherAssignmentsAsync();
    
    Task<ResponseDto<ClassSubjectResponseDto>> CreateClassSubjectAsync(CreateClassSubjectDto classSubject);
    
    Task<ResponseDto<TeacherAssignmentResponseDto>> CreateTeacherAssignmentAsync(CreateTeacherAssignmentDto teacherAssignment);
}