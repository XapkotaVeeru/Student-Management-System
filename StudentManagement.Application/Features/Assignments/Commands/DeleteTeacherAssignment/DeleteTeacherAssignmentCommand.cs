using MediatR;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Assignments.Commands.DeleteTeacherAssignment;

public class DeleteTeacherAssignmentCommand : IRequest<ResponseDto<bool>>
{
    public int AssignmentId { get; set; }
    
    public DeleteTeacherAssignmentCommand(int assignmentId)
    {
        AssignmentId = assignmentId;
    }
}