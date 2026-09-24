using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Enums;

namespace StudentManagement.Application.Features.Users.Commands.DeactivateUser;

public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, ResponseDto<UserResponseDto>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IApplicationDbContext _dbContext;

    public DeactivateUserCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext dbContext)
    {
        _currentUserService = currentUserService;
        _dbContext = dbContext;
    }
    
    public async Task<ResponseDto<UserResponseDto>> Handle(DeactivateUserCommand request, 
        CancellationToken cancellationToken)
    {
        var admin = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId);

        if (admin == null)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }

        var userIsActive = await _dbContext.Users.Where(x => x.Id == request.UserId)
            .Select(x => x.IsActive)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (!userIsActive)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User has been deactivated",
                Data = null
            };
        }
        
        var user = await _dbContext.Users.FirstOrDefaultAsync
            (x => x.Id == request.UserId, cancellationToken: cancellationToken);

        if (user == null)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }

        user.IsActive = false;
        user.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ResponseDto<UserResponseDto>
        {
            Status = true,
            Message = "User has been deactivated",
            Data = new UserResponseDto
            {
                CreatedAt =  user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Email = user.Email,
                IsActive = user.IsActive,
                Id = user.Id,
                Username = user.Username,
                UserRole = user.UserRole
            }
        };
    }
}