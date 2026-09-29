using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.Classes;
using StudentManagement.Application.Features.Classes.Commands.ActivateClass;
using StudentManagement.Application.Features.Classes.Commands.CreateClass;
using StudentManagement.Application.Features.Classes.Commands.DeactivateClass;
using StudentManagement.Application.Features.Classes.Commands.UpdateClass;
using StudentManagement.Application.Features.Classes.Queries.GetClassById;
using StudentManagement.Application.Features.Classes.Queries.GetClasses;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ClassController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClassController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClass(CreateClassDto dto, CancellationToken cancellationToken)
    {
        var command = new CreateClassCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetClasses(
        CancellationToken cancellationToken)
    {
        var query = new GetClassesQuery();

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClassById(int id, CancellationToken cancellationToken)
    {
        var query = new GetClassByIdQuery(id);

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClass(int id, UpdateClassDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateClassCommand(id, dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpPut("{id}/deactivate")]
    public async Task<IActionResult> DeactivateClass(int id, CancellationToken cancellationToken)
    {
        var command = new DeactivateClassCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpPut("{id}/activate")]
    public async Task<IActionResult> ActivateClass(int id, CancellationToken cancellationToken)
    {
        var command = new ActivateClassCommand(id);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}