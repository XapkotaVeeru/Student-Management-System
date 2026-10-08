using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.Features.Assignments.Commands.CreateTeacherAssignment;
using StudentManagement.Application.Features.Assignments.Commands.DeleteTeacherAssignment;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AssignmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssignmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("teacher")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateTeacherAssignment(CreateTeacherAssignmentDto dto, 
        CancellationToken cancellationToken)
    {
        var command = new CreateTeacherAssignmentCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpDelete("teacher/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteTeacherAssignment(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteTeacherAssignmentCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}