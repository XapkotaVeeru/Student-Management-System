using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Users;
using StudentManagement.Application.Features.Users.Commands.ApproveUser;
using StudentManagement.Application.Interfaces;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("{id}/approve")]
    public async Task<IActionResult> Approve(int id, ApproveUserDto dto, CancellationToken cancellationToken)
    {
        var command = new ApproveUserCommand(id, dto);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
    

}