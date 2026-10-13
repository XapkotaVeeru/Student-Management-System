using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using StudentManagement.Application.DTOs.Common;

namespace Student_Management_System.Middleware;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
            return false;

        var message = string.Join("; ",
            validationException.Errors.Select(e => e.ErrorMessage).Distinct());

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(
            new ResponseDto<object> { Status = false, Message = message, Data = null },
            cancellationToken);

        return true;
    }
}