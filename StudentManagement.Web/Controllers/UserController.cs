using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Users;
using StudentManagement.Application.Features.Users.Commands.ApproveUser;
using StudentManagement.Application.Features.Users.Commands.CreateUser;
using StudentManagement.Application.Features.Users.Commands.DeactivateUser;
using StudentManagement.Application.Features.Users.Queries.GetUserById;
using StudentManagement.Application.Features.Users.Queries.GetUsers;
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
    
    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery();

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet("{id}")]

    public async Task<IActionResult> GetUser(int id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateUserCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> DeactivateUser(int id, CancellationToken cancellationToken)
    {
        var command = new DeactivateUserCommand(id);
        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}