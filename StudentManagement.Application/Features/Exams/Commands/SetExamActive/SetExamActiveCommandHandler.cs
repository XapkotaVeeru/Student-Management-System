using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Exams.Commands.SetExamActive;

public class SetExamActiveCommandHandler : IRequestHandler<SetExamActiveCommand, ResponseDto<bool>>
{
    private readonly IApplicationDbContext _context;

    public SetExamActiveCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseDto<bool>> Handle(SetExamActiveCommand request, CancellationToken cancellationToken)
    {
        var exam = await _context.Exams.FirstOrDefaultAsync(
            x => x.Id == request.ExamId, cancellationToken);

        if (exam == null)
        {
            return new ResponseDto<bool> 
                { 
                    Status = false, 
                    Message = "Exam not found", 
                    Data = false 
                };
        }

        if (exam.IsActive == request.IsActive)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = request.IsActive ? "Exam is already active" : "Exam is already inactive",
                Data = false
            };
        }

        exam.IsActive = request.IsActive;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<bool>
        {
            Status = true,
            Message = request.IsActive ? "Exam activated successfully" : "Exam deactivated successfully",
            Data = true
        };
    }
}