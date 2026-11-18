using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using StudentManagement.Application.DTOs.Common;

namespace Student_Management_System.Middleware;

public class DbUpdateExceptionHandler : IExceptionHandler
{
    private readonly ILogger<DbUpdateExceptionHandler> _logger;

    public DbUpdateExceptionHandler(ILogger<DbUpdateExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    { 
        if (exception is not DbUpdateException dbException
          || dbException.InnerException is not PostgresException pg
          || pg.SqlState != PostgresErrorCodes.UniqueViolation)
        {
            return false;
        }

        _logger.LogWarning("Unique constraint violated: {Constraint}", pg.ConstraintName);

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

        await httpContext.Response.WriteAsJsonAsync(
            new ResponseDto<object>
            {
                Status = false,
                Message = "This record already exists, or another request just created it. Please refresh and try again.",
                Data = null
            },
            cancellationToken);

        return true;
    }
}