using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Assignments;
using StudentManagement.Application.Features.Assignments.Commands.CreateClassSubject;
using StudentManagement.Application.Features.Assignments.Commands.CreateTeacherAssignment;
using StudentManagement.Application.Features.Assignments.Commands.DeleteClassSubject;
using StudentManagement.Application.Features.Assignments.Commands.DeleteTeacherAssignment;
using StudentManagement.Application.Features.Assignments.Queries.GetClassSubjects;
using StudentManagement.Application.Features.Assignments.Queries.GetTeacherAssignments;

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

    [HttpGet("teacher")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetTeacherAssignments(int? teacherId, int? classId,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetTeacherAssignmentsQuery(teacherId, classId), cancellationToken);

        return Ok(result);
    }

    [HttpPost("class-subject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateClassSubject(CreateClassSubjectDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new CreateClassSubjectCommand(dto), cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("class-subject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetClassSubjects(int? classId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetClassSubjectsQuery(classId), cancellationToken);

        return Ok(result);
    }

    [HttpDelete("class-subject/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteClassSubject(int id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteClassSubjectCommand(id), cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}