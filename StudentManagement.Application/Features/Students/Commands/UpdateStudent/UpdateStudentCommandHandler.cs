using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Students.Commands.UpdateStudent;

public class UpdateStudentCommandHandler
    : IRequestHandler<UpdateStudentCommand, ResponseDto<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateStudentCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<bool>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _context.Students.FirstOrDefaultAsync(
                x => x.Id == request.StudentId, cancellationToken);

        if (student == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Student not found",
                Data = false
            };
        }

        if (_currentUserService.UserRole != UserRole.Admin && _currentUserService.UserRole != UserRole.Student)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "You are not allowed to update student profiles",
                Data = false
            };
        }

        if (_currentUserService.UserRole == UserRole.Student && student.UserId != _currentUserService.UserId)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "You can only update your own profile",
                Data = false
            };
        }

        var emailExists = await _context.Students.AnyAsync(
            x => x.Email == request.Student.Email &&
                     x.Id != student.Id, cancellationToken);

        if (emailExists)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Email already exists",
                Data = false
            };
        }
        
        student.FirstName = request.Student.FirstName;
        student.LastName = request.Student.LastName;
        student.Email = request.Student.Email;
        student.DateOfBirth = request.Student.DateOfBirth;
        student.PhoneNumber = request.Student.PhoneNumber;
        student.Gender = request.Student.Gender;
        student.Address = request.Student.Address;
        
        if (_currentUserService.UserRole == UserRole.Admin)
        {
            if (request.Student.SchoolClassId == null)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    Message = "School class is required for admin update",
                    Data = false
                };
            }

            var schoolClassExists = await _context.SchoolClasses
                .AnyAsync(
                    x => x.Id == request.Student.SchoolClassId &&
                         x.IsActive,
                    cancellationToken);

            if (!schoolClassExists)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    Message = "School class not found or inactive",
                    Data = false
                };
            }

            student.SchoolClassId = request.Student.SchoolClassId;
        }
        
        var user = await _context.Users
            .FirstOrDefaultAsync(
                x => x.Id == student.UserId,
                cancellationToken);

        if (user == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Student account not found",
                Data = false
            };
        }

        user.Email = request.Student.Email;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<bool>
        {
            Status = true,
            Message = "Student updated successfully",
            Data = true
        };
    }
}