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
        
        if (_currentUserService.UserRole != UserRole.Admin &&
            _currentUserService.UserRole != UserRole.Student)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "You are not allowed to update student profiles",
                Data = false
            };
        }
        
        if (_currentUserService.UserRole == UserRole.Student &&
            student.UserId != _currentUserService.UserId)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "You can only update your own profile",
                Data = false
            };
        }
        
        var user = await _context.Users.FirstOrDefaultAsync(
                x => x.Id == student.UserId, cancellationToken);

        if (user == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Student account not found",
                Data = false
            };
        }

        if (request.Student.FirstName != null)
        {
            student.FirstName = request.Student.FirstName;
        }

        if (request.Student.LastName != null)
        {
            student.LastName = request.Student.LastName;
        }

        if (request.Student.DateOfBirth.HasValue)
        {
            student.DateOfBirth = request.Student.DateOfBirth;
        }

        if (request.Student.PhoneNumber != null)
        {
            student.PhoneNumber = request.Student.PhoneNumber;
        }

        if (request.Student.Gender.HasValue)
        {
            student.Gender = request.Student.Gender;
        }

        if (request.Student.Address != null)
        {
            student.Address = request.Student.Address;
        }

        if (request.Student.Email != null)
        { 
            var studentEmailExists = await _context.Students.AnyAsync(x => x.Email
                    == request.Student.Email && x.Id != student.Id, cancellationToken);

            if (studentEmailExists)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    Message = "Email already exists",
                    Data = false
                };
            }

            var userEmailExists = await _context.Users.AnyAsync(
                    x => x.Email == request.Student.Email && x.Id != user.Id, cancellationToken);

            if (userEmailExists)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    Message = "Email already exists",
                    Data = false
                };
            }

            student.Email = request.Student.Email;
            user.Email = request.Student.Email;
        }

        if (_currentUserService.UserRole == UserRole.Admin)
        {
            if (request.Student.SchoolClassId.HasValue)
            {
                var schoolClassExists = await _context.SchoolClasses.AnyAsync(
                        x => x.Id == request.Student.SchoolClassId.Value && x.IsActive,
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

                student.SchoolClassId =
                    request.Student.SchoolClassId.Value;
            }
            
            if (request.Student.AdmissionNumber != null)
            {
                student.AdmissionNumber =
                    request.Student.AdmissionNumber;
            }
        }
        
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