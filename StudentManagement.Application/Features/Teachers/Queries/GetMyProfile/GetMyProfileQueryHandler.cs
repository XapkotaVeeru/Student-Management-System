using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Teachers;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Teachers.Queries.GetMyProfile;

public class GetMyTeacherProfileQueryHandler : IRequestHandler<GetMyTeacherProfileQuery, 
    ResponseDto<TeacherResponseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetMyTeacherProfileQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<TeacherResponseDto>> Handle(GetMyTeacherProfileQuery request, 
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (!userId.HasValue)
        {
            return new ResponseDto<TeacherResponseDto>
            {
                Status = false,
                Message = "User is not authenticated",
                Data = null
            };
        }

        var teacher = await _context.Teachers.FirstOrDefaultAsync(
                x => x.UserId == userId.Value, cancellationToken);

        if (teacher == null)
        {
            return new ResponseDto<TeacherResponseDto>
            {
                Status = false,
                Message = "Teacher profile not found",
                Data = null
            };
        }

        return new ResponseDto<TeacherResponseDto>
        {
            Status = true,
            Message = "Teacher profile retrieved successfully",
            Data = new TeacherResponseDto
            {
                Id = teacher.Id,
                EmployeeNumber = teacher.EmployeeNumber,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                Email = teacher.Email,
                PhoneNumber = teacher.PhoneNumber,
                IsActive = teacher.IsActive
            }
        };
    }
}