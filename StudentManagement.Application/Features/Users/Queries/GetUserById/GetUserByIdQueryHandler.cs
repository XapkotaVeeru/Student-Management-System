using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;
using StudentManagement.Application.DTOs.Users;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseDto<UserResponseDto>>
{
    private readonly IApplicationDbContext _dbContext;
    
    public GetUserByIdQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ResponseDto<UserResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id, 
            cancellationToken: cancellationToken);

        if (user == null)
        {
            return new ResponseDto<UserResponseDto>
            {
                Status = false,
                Message = "User not found",
                Data = null
            };
        }

        var response = new UserResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            CreatedAt = user.CreatedAt,
            Email = user.Email,
            IsActive = user.IsActive,
            UpdatedAt = user.UpdatedAt,
            UserRole = user.UserRole
        };

        return new ResponseDto<UserResponseDto>
        {
            Status = true,
            Data = response,
            Message = "User successfully retrieved"
        };
    }
}