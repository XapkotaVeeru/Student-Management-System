using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StudentManagement.Application.Interfaces;

namespace StudentManagement.Infrastructure.BackgroundServices;

public class RefreshTokenCleanupService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<RefreshTokenCleanupService> _logger;

    public RefreshTokenCleanupService(IServiceScopeFactory scopeFactory, ILogger<RefreshTokenCleanupService> logger)
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

                var expiredTokens = await context.RefreshTokens
                    .Where(x => x.ExpiresAt <= DateTime.UtcNow)
                    .ToListAsync(cancellationToken);

                if (expiredTokens.Count > 0)
                {
                    context.RefreshTokens.RemoveRange(expiredTokens);

                    await context.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation("Deleted {Count} expired refresh tokens.", expiredTokens.Count);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while cleaning up expired refresh tokens.");
            }

            await Task.Delay(TimeSpan.FromHours(1), cancellationToken);
        }
    }
}