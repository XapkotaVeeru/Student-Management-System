using Microsoft.EntityFrameworkCore;
using StudentManagement.Application.DTOs.Common;

namespace StudentManagement.Application.Features.Common;

public static class PaginationExtensions
{
    public const int DefaultPageSize = 20;
    public const int MaxPageSize = 100;
    private const int MaxPage = 100_000;
    
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query, int? page, int? pageSize, CancellationToken cancellationToken)
    {
        var currentPage = (page is null or < 1) ? 1 : Math.Min(page.Value, MaxPage);
        var size = (pageSize is null or < 1) ? DefaultPageSize : Math.Min(pageSize.Value, MaxPageSize);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((currentPage - 1) * size)
            .Take(size)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>
        {
            Items = items,
            Page = currentPage,
            PageSize = size,
            TotalCount = totalCount
        };
    }
}