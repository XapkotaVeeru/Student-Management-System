using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Commands.UpdateTeacher;

public class UpdateTeacherCommand : IRequest<ResponseDto<bool>>
{
    public int TeacherId { get; }
    public UpdateTeacherDto Teacher { get; }
    
    public UpdateTeacherCommand(int teacherId, UpdateTeacherDto teacher)
    {
        TeacherId = teacherId;
        Teacher = teacher;
    }
}