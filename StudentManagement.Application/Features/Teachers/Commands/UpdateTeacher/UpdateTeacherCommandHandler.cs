using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Teachers.Commands.UpdateTeacher;

public class UpdateTeacherCommandHandler
    : IRequestHandler<UpdateTeacherCommand, ResponseDto<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public UpdateTeacherCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<ResponseDto<bool>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
    {
        var teacher = await _context.Teachers.FirstOrDefaultAsync(
                x => x.Id == request.TeacherId, cancellationToken);

        if (teacher == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Teacher not found",
                Data = false
            };
        }

        if (_currentUserService.UserRole != UserRole.Admin &&
            _currentUserService.UserRole != UserRole.Teacher)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "You are not allowed to update teacher profiles",
                Data = false
            };
        }

        if (_currentUserService.UserRole == UserRole.Teacher && teacher.UserId != _currentUserService.UserId)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "You can only update your own profile",
                Data = false
            };
        }

        var user = await _context.Users.FirstOrDefaultAsync(
                x => x.Id == teacher.UserId, cancellationToken);

        if (user == null)
        {
            return new ResponseDto<bool>
            {
                Status = false,
                Message = "Teacher account not found",
                Data = false
            };
        }

        if (request.Teacher.FirstName != null)
        {
            teacher.FirstName = request.Teacher.FirstName;
        }

        if (request.Teacher.LastName != null)
        {
            teacher.LastName = request.Teacher.LastName;
        }

        if (request.Teacher.PhoneNumber != null)
        {
            teacher.PhoneNumber = request.Teacher.PhoneNumber;
        }

        if (request.Teacher.Gender.HasValue)
        {
            teacher.Gender = request.Teacher.Gender;
        }


        if (request.Teacher.Email != null)
        {
            var teacherEmailExists = await _context.Teachers.AnyAsync(x => x.Email == 
                request.Teacher.Email && x.Id != teacher.Id, cancellationToken);

            if (teacherEmailExists)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    Message = "Email already exists",
                    Data = false
                };
            }

            var userEmailExists = await _context.Users.AnyAsync(
                   x => x.Email == request.Teacher.Email && x.Id != user.Id,
                    cancellationToken);

            if (userEmailExists)
            {
                return new ResponseDto<bool>
                {
                    Status = false,
                    Message = "Email already exists",
                    Data = false
                };
            }

            teacher.Email = request.Teacher.Email;
            user.Email = request.Teacher.Email;
        }

        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return new ResponseDto<bool>
        {
            Status = true,
            Message = "Teacher updated successfully",
            Data = true
        };
    }
}