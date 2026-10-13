using System.Security.Claims;
using StudentManagement.Application.Interfaces;
using StudentManagement.Domain.Entities;

namespace Student_Management_System.Middleware;

public class AccessLogMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AccessLogMiddleware> _logger;

    public AccessLogMiddleware(RequestDelegate next, IServiceScopeFactory scopeFactory,
        ILogger<AccessLogMiddleware> logger)
    {
        _next = next;
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Only log API calls, not Swagger pages or favicon requests
        if (!context.Request.Path.StartsWithSegments("/api"))
        {
            await _next(context);
            return;
        }

        await _next(context);   // run the rest of the pipeline first

        await SaveLogAsync(context);   // then log, so we know the final status code and user
    }

    private async Task SaveLogAsync(HttpContext context)
    {
        try
        {
            var userIdClaim = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            int? userId = int.TryParse(userIdClaim, out var id) ? id : null;

            var endpoint = context.Request.Path.Value ?? string.Empty;
            if (endpoint.Length > 500)
                endpoint = endpoint[..500];

            // A separate scope = a separate DbContext, so we never save
            // half-finished changes from the request itself
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

            await dbContext.AccessLogs.AddAsync(new AccessLog
            {
                UserId = userId,
                HttpMethod = context.Request.Method,
                Endpoint = endpoint,
                StatusCode = context.Response.StatusCode,
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                Timestamp = DateTime.UtcNow
            });

            await dbContext.SaveChangesAsync(CancellationToken.None);
        }
        catch (Exception ex)
        {
            // Logging must never break the actual request
            _logger.LogError(ex, "Failed to write access log");
        }
    }
}