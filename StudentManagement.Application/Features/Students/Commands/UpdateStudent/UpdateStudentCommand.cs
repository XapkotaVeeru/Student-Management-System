using MediatR;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;

namespace StudentManagement.Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentCommand : IRequest<ResponseDto<bool>>
{
    public int StudentId { get; }
    public UpdateStudentDto Student { get; }

    public UpdateStudentCommand(int studentId, UpdateStudentDto student)
    {
        StudentId = studentId;
        Student = student;
    }
}