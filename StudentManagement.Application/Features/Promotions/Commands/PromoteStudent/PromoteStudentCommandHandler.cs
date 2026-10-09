using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Promotions;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Promotions.Commands.PromoteStudent;

public class PromoteStudentCommandHandler
    : IRequestHandler<PromoteStudentCommand, ResponseDto<PromotionResponseDto>>
{
    private const decimal PassPercentage = 40m; 

    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public PromoteStudentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<PromotionResponseDto>> Handle(PromoteStudentCommand request, 
        CancellationToken cancellationToken)
    {
        var adminUserId = _currentUserService.UserId;

        if (adminUserId == null)
        {
            return Fail("User not authenticated");
        }
        
        var dto = request.Request;

        if (dto.FromSchoolClassId == dto.ToSchoolClassId)
        {
            return Fail("Target class must be different from the current class");
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(x => x.Id == dto.StudentId && x.IsActive, cancellationToken);

        if (student == null)
        {
            return Fail("Student not found");
        }

        if (student.SchoolClassId != dto.FromSchoolClassId)
        {
            return Fail("Student is not in the specified class");
        }
        
        var toClass = await _context.SchoolClasses
            .FirstOrDefaultAsync(x => x.Id == dto.ToSchoolClassId && x.IsActive, cancellationToken);

        if (toClass == null)
        {
            return Fail("Target class not found or inactive");
        }
        
        var alreadyExists = await _context.StudentPromotions.AnyAsync(
            x => x.StudentId == student.Id
                 && x.AcademicYear == dto.AcademicYear
                 && x.Status != PromotionStatus.Rejected,
            cancellationToken);

        if (alreadyExists)
            return Fail("A promotion already exists for this student and academic year");

        var marks = await _context.ExamMarks
            .Where(m => m.StudentId == student.Id
                        && m.Status == MarkStatus.Approved
                        && m.IsPublished
                        && _context.Exams.Any(e => e.Id == m.ExamId
                                                   && e.SchoolClassId == dto.FromSchoolClassId))
            .ToListAsync(cancellationToken);

        if (!PromotionRules.HasPassedAll(marks, PassPercentage))
        {
            return Fail("Student has not met the pass criteria");
        }


        var promotion = new StudentPromotion
        {
            StudentId = student.Id,
            FromSchoolClassId = dto.FromSchoolClassId,
            ToSchoolClassId = dto.ToSchoolClassId,
            AcademicYear = dto.AcademicYear,
            Status = PromotionStatus.Approved,
            ReviewedByUserId = adminUserId,
            PromotionDate = DateTime.UtcNow
        };

        student.SchoolClassId = dto.ToSchoolClassId;   

        await _context.StudentPromotions.AddAsync(promotion, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);   

        return new ResponseDto<PromotionResponseDto>
        {
            Status = true,
            Message = "Promotion created",
            Data = new PromotionResponseDto
            {
                Id = promotion.Id,
                StudentId = promotion.StudentId,
                FromSchoolClassId = promotion.FromSchoolClassId,
                ToSchoolClassId = promotion.ToSchoolClassId,
                AcademicYear = promotion.AcademicYear,
                PromotionDate = promotion.PromotionDate,
                Status = promotion.Status,
                ApprovedByUserId = promotion.ReviewedByUserId
            }
        };
    }

    private static ResponseDto<PromotionResponseDto> Fail(string message) => new()
    {
        Status = false,
        Message = message,
        Data = null
    };
}