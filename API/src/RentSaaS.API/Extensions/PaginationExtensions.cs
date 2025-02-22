using Microsoft.EntityFrameworkCore;
using RentSaaS.API.Models;
using System.Linq;

namespace RentSaaS.API.Extensions;

public static class PaginationExtensions
{
    public static async Task<(List<T> Items, PaginationInfo Pagination)> ToPaginatedListAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        var pagination = new PaginationInfo
        {
            CurrentPage = pageNumber,
            PageSize = pageSize
        };

        pagination.Validate();

        var totalItems = await query.CountAsync();
        pagination.TotalItems = totalItems;

        var items = await query
            .Skip(pagination.Skip)
            .Take(pagination.PageSize)
            .ToListAsync();

        return (items, pagination);
    }

    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        int pageNumber,
        int pageSize)
    {
        var result = new PagedResult<T>();
        result.Pagination = new PaginationInfo(pageNumber, pageSize, 0);
        result.Pagination.Validate();

        result.Pagination.TotalItems = await query.CountAsync();

        result.Items = await query
            .Skip(result.Pagination.Skip)
            .Take(result.Pagination.PageSize)
            .ToListAsync();

        return result;
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public PaginationInfo Pagination { get; set; } = new PaginationInfo();
}