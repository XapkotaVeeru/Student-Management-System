using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Infrastructure.BackgroundServices;

public class AccessLogCleanupService : BackgroundService
{
    private const int RetentionDays = 90;

    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<AccessLogCleanupService> _logger;

    public AccessLogCleanupService(IServiceScopeFactory scopeFactory, ILogger<AccessLogCleanupService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();

                var context = scope.ServiceProvider
                    .GetRequiredService<IApplicationDbContext>();

                var cutoff = DateTime.UtcNow.AddDays(-RetentionDays);

                var deleted = await context.AccessLogs
                    .Where(x => x.Timestamp < cutoff)
                    .ExecuteDeleteAsync(cancellationToken);

                if (deleted > 0)
                {
                    _logger.LogInformation(
                        "Deleted {Count} access log rows older than {Days} days.", deleted, RetentionDays);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while cleaning up access logs.");
            }

            await Task.Delay(TimeSpan.FromHours(24), cancellationToken);
        }
    }
}