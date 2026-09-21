using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Students;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Students.Queries.GetMyProfile;

public class GetMyProfileQueryHandler :IRequestHandler<GetMyProfileQuery, ResponseDto<StudentResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public GetMyProfileQueryHandler(IApplicationDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<StudentResponseDto>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
    {
        if (_currentUserService.UserId == null)
        {
            return new ResponseDto<StudentResponseDto>
            {
                Status = false,
                Message = "User not authenticated",
                Data = null
            };
        }

        var student = await _dbContext.Students.FirstOrDefaultAsync
            (x => x.UserId == _currentUserService.UserId, cancellationToken: cancellationToken);

        if (student == null)
        {
            return new ResponseDto<StudentResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }

        var response = new StudentResponseDto
        {
            Id = student.Id,
            StudentNumber = student.StudentNumber,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Address = student.Address,
            IsActive = student.IsActive,
            DateOfBirth = student.DateOfBirth,
            PhoneNumber = student.PhoneNumber,
            Gender =  student.Gender,
            SchoolClassId =  student.SchoolClassId,
            EnrollmentDate =  student.EnrollmentDate,
            AdmissionNumber =  student.AdmissionNumber
        };

        return new ResponseDto<StudentResponseDto>
        {
            Status = true,
            Message = "Student Profile retrieved successfully",
            Data = response
        };

    }
}