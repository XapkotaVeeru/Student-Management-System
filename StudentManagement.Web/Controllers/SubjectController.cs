using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Subjects;
using StudentManagement.Application.Features.Subjects.Commands.ActivateSubject;
using StudentManagement.Application.Features.Subjects.Commands.CreateSubject;
using StudentManagement.Application.Features.Subjects.Commands.DeactivateSubject;
using StudentManagement.Application.Features.Subjects.Commands.UpdateSubject;
using StudentManagement.Application.Features.Subjects.Queries.GetAllSubjects;
using StudentManagement.Application.Features.Subjects.Queries.GetSubjectById;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class SubjectController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubjectController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubject(CreateSubjectDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateSubjectCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetSubjects(CancellationToken cancellationToken)
    {
        var query = new GetSubjectsQuery();

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectById(int id, CancellationToken cancellationToken)
    {
        var query = new GetSubjectByIdQuery(id);

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateSubjectCommand(id, dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> DeactivateSubject(int id, CancellationToken cancellationToken)
    {
        var command = new DeactivateSubjectCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpPut("{id}/activate")]
    public async Task<IActionResult> ActivateSubject(int id, CancellationToken cancellationToken)
    {
        var command = new ActivateSubjectCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}