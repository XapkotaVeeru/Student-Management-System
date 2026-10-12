using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Commands.DeactivateTeacher;

public class DeactivateTeacherCommand : IRequest<ResponseDto<TeacherResponseDto>>
{
    public int TeacherId { get; }

    public DeactivateTeacherCommand(int teacherId)
    {
        TeacherId = teacherId;
    }
}