using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.DTOs.ReExam;
using StudentManagement.Application.DTOs.Students;
using StudentManagement.Application.Features.ReExams.Commands.ApplyReExam;
using StudentManagement.Application.Features.Students.Commands.UpdateStudent;
using StudentManagement.Application.Features.Students.Queries.GetMyProfile;
using StudentManagement.Application.Features.Students.Queries.GetMyResults;
using StudentManagement.Application.Features.Students.Queries.GetMySubjects;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> GetMyProfile(
        CancellationToken cancellationToken)
    {
        var query = new GetMyProfileQuery();

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
    
    [HttpGet("subjects")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> GetMySubjects(
        CancellationToken cancellationToken)
    {
        var query = new GetMySubjectsQuery();

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
    
    [HttpGet("results")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> GetMyResults(
        CancellationToken cancellationToken)
    {
        var query = new GetMyResultsQuery();

        var result = await _mediator.Send(query, cancellationToken);

        if (!result.Status)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPost("re-exams")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> ApplyForReExam(ApplyReExamDto dto, CancellationToken cancellationToken)
    {
        var command = new ApplyReExamCommand(dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Student")]
    public async Task<IActionResult> UpdateStudent(int id, UpdateStudentDto dto, CancellationToken cancellationToken)
    {
        var command = new UpdateStudentCommand(id, dto);

        var result = await _mediator.Send(command, cancellationToken);

        if (!result.Status)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }
}