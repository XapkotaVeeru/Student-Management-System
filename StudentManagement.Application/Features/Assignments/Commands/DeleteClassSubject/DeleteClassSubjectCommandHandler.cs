using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Assignments.Commands.DeleteClassSubject;

public class DeleteClassSubjectCommandHandler : IRequestHandler<DeleteClassSubjectCommand, ResponseDto<bool>>
{
    private readonly IApplicationDbContext _context;

    public DeleteClassSubjectCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<bool>> Handle(DeleteClassSubjectCommand request,
        CancellationToken cancellationToken)
    {
        var classSubject = await _context.ClassSubjects.FirstOrDefaultAsync(
            x => x.Id == request.ClassSubjectId, cancellationToken);

        if (classSubject == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Class-subject link not found",
                Data = false
            };
        }

        var teachersAssigned = await _context.TeacherAssignments.AnyAsync(
            x => x.SchoolClassId == classSubject.SchoolClassId && x.SubjectId == classSubject.SubjectId,
            cancellationToken);

        if (teachersAssigned)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Cannot remove this link while teachers are assigned to it. Remove the teacher assignments first.",
                Data = false
            };
        }

        _context.ClassSubjects.Remove(classSubject);
        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<bool>
        {
            Status = true,
            Message = "Class-subject link deleted successfully",
            Data = true
        };
    }
}