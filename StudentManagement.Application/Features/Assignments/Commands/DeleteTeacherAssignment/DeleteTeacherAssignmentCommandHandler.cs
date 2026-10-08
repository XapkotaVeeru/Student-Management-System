using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Assignments.Commands.DeleteTeacherAssignment;

public class DeleteTeacherAssignmentCommandHandler : IRequestHandler<DeleteTeacherAssignmentCommand, 
    ResponseDto<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteTeacherAssignmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<bool>> Handle(DeleteTeacherAssignmentCommand request,
        CancellationToken cancellationToken)
    {
        var assignment = await _context.TeacherAssignments.FirstOrDefaultAsync(x => 
                x.Id == request.AssignmentId, cancellationToken);

        if (assignment == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Assignment not found",
                Data = false
            };
        }

        _context.TeacherAssignments.Remove(assignment);

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<bool>
        {
            Status = true,
            Message = "Assignment deleted successfully",
            Data = true
        };
    }
}