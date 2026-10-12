using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;

namespace StudentManagement.Application.Features.Students.Commands.DeactivateStudent;

public class DeactivateStudentCommand : IRequest<ResponseDto<StudentResponseDto>>
{
    public int StudentId { get; }

    public DeactivateStudentCommand(int studentId)
    {
        StudentId = studentId;
    }
}