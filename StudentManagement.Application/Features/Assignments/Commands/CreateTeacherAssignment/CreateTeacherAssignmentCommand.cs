using MediatR;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Application.Features.Assignments.Commands.CreateTeacherAssignment;

public class CreateTeacherAssignmentCommand : IRequest<ResponseDto<TeacherAssignmentResponseDto>>
{
    public CreateTeacherAssignmentDto Assignment { get;}
    
    public CreateTeacherAssignmentCommand(CreateTeacherAssignmentDto assignment)
    {
        Assignment = assignment;
    }
}