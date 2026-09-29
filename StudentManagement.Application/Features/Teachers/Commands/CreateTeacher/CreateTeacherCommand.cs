using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;

namespace StudentManagement.Application.Features.Teachers.Commands.CreateTeacher;

public class CreateTeacherCommand : IRequest<ResponseDto<TeacherResponseDto>>
{
    public CreateTeacherDto Request { get; }

    public CreateTeacherCommand(CreateTeacherDto request)
    {
        Request = request;
    }
}