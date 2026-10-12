using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Commands.ActivateTeacher;

public class ActivateTeacherCommand : IRequest<ResponseDto<TeacherResponseDto>>
{
    public int TeacherId { get; }

    public ActivateTeacherCommand(int teacherId)
    {
        TeacherId = teacherId;
    }
}