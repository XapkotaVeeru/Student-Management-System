using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Application.Features.Students.Queries.GetMyProfile;
using StudentManagement.Application.Features.Students.Queries.GetMyResults;
using StudentManagement.Application.Features.Students.Queries.GetMySubjects;

namespace Student_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Student")]
public class StudentController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("me")]
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
}