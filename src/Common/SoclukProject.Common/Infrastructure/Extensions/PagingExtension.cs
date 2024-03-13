using Microsoft.EntityFrameworkCore;
using SoclukProject.Common.Models.Pages;

namespace SoclukProject.Common.Infrastructure.Extensions;
public static class PagingExtension
{
    public static async Task<PagedViewmodel<T>> ToPaged<T>(this IQueryable<T> query,
                                                            int currentPage,
                                                            int pageSize) where T : class
    {
        var count = await query.CountAsync();

        Page paging = new(currentPage, pageSize, count);

        var data = await query.Skip(paging.Skip).Take(paging.PageSize).AsNoTracking().ToListAsync();

        var result = new PagedViewmodel<T>(data, paging);

        return result;
    }
}
