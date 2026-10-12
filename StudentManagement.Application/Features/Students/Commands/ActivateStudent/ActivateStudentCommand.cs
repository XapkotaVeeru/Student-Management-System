using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;

namespace StudentManagement.Application.Features.Students.Commands.ActivateStudent;

public class ActivateStudentCommand:IRequest<ResponseDto<StudentResponseDto>>
{
    public int StudentId { get; }
    public ActivateStudentCommand(int studentId)
    {
        StudentId = studentId;
    }
}