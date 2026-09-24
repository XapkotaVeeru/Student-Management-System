using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ResponseDto<IEnumerable<UserResponseDto>>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetUsersQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResponseDto<IEnumerable<UserResponseDto>>> Handle(GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users.Select(user => new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            IsActive = user.IsActive,
            UserRole = user.UserRole,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        }).ToListAsync(cancellationToken);

        return new ResponseDto<IEnumerable<UserResponseDto>>
        {
            Data = users,
            Message = "Success",
            Status = true
        };
    }
}