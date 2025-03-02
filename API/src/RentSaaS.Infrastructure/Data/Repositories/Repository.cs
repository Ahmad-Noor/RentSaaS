using RentSaaS.Domain.Base;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RentSaaS.Infrastructure.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected readonly DbSet<T> _dbSet;
    protected readonly RentSaaSDBContext _context;
    protected readonly ILogger<Repository<T>> _logger;

    public Repository(RentSaaSDBContext context, ILogger<Repository<T>> logger)
    {
        _context = context;
        _dbSet = _context.Set<T>();
        _logger = logger;
    }

    public IQueryable<T> AsQueryable() =>
        _dbSet.AsNoTracking().Where(x => !x.IsDeleted);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbSet.AsNoTracking()
                .Where(c => !c.IsDeleted)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity}", nameof(GetAllAsync), typeof(T).Name);
            throw;
        }
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
            {
                _logger.LogWarning("Entity of type {Entity} with id {Id} was not found", typeof(T).Name, id);
            }
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity} with id {Id}", nameof(GetByIdAsync), typeof(T).Name, id);
            throw;
        }
    }

    public virtual async Task<bool> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding an entity of type {Entity}", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task<bool> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding multiple entities of type {Entity}", typeof(T).Name);
            throw;
        }
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        try
        {
              _dbSet.RemoveRange(entities); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding multiple entities of type {Entity}", typeof(T).Name);
            throw;
        }
    }

    public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating an entity of type {Entity}", typeof(T).Name);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await _dbSet.FindAsync(new object[] { id }, cancellationToken);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return true;
            }
            else
            {
                _logger.LogWarning("Entity of type {Entity} with id {Id} not found for deletion", typeof(T).Name, id);
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting an entity of type {Entity} with id {Id}", typeof(T).Name, id);
            throw;
        }
    }

    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbSet.SingleOrDefaultAsync(predicate, cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity}", nameof(SingleOrDefaultAsync), typeof(T).Name);
            throw;
        }
    }

    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity}", nameof(FirstOrDefaultAsync), typeof(T).Name);
            throw;
        }
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _dbSet.Where(predicate).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity}", nameof(FindAsync), typeof(T).Name);
            throw;
        }
    }

    public async Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        // Alias for FindAsync
        return await FindAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllPaginatedAsync(int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        try
        {
            page = Math.Max(page, 1);
            pageSize = Math.Max(pageSize, 1);

            return await _dbSet
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity}", nameof(GetAllPaginatedAsync), typeof(T).Name);
            throw;
        }
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<T> query = _dbSet.AsNoTracking().Where(e => !e.IsDeleted);

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity}", nameof(CountAsync), typeof(T).Name);
            throw;
        }
    }

    public async Task<IEnumerable<T>> GetAllPaginatedFilteredAsync(
        Expression<Func<T, bool>> filter,
        int page = 1,
        int pageSize = 10,
        string? sortBy = null,
        bool isAscending = true,
        CancellationToken cancellationToken = default)
    {
        try
        {
            page = Math.Max(page, 1);
            pageSize = Math.Max(pageSize, 1);

            IQueryable<T> query = _dbSet.AsNoTracking()
                .Where(e => !e.IsDeleted)
                .Where(filter);

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = ApplySorting(query, sortBy, isAscending);
            }
            else
            {
                query = isAscending
                    ? query.OrderBy(e => e.CreatedAt)
                    : query.OrderByDescending(e => e.CreatedAt);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity}", nameof(GetAllPaginatedFilteredAsync), typeof(T).Name);
            throw;
        }
    }

    private IQueryable<T> ApplySorting(IQueryable<T> query, string sortBy, bool isAscending)
    {
        var property = typeof(T).GetProperty(
            sortBy,
            System.Reflection.BindingFlags.IgnoreCase |
            System.Reflection.BindingFlags.Public |
            System.Reflection.BindingFlags.Instance);

        if (property == null)
        {
            _logger.LogWarning("Property {SortBy} not found in type {Entity}. Defaulting to CreatedAt.", sortBy, typeof(T).Name);
            return isAscending ? query.OrderBy(e => e.CreatedAt) : query.OrderByDescending(e => e.CreatedAt);
        }

        var parameter = Expression.Parameter(typeof(T), "x");
        var propertyAccess = Expression.Property(parameter, property);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        var methodName = isAscending ? "OrderBy" : "OrderByDescending";

        var resultExp = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.PropertyType },
            query.Expression,
            Expression.Quote(orderByExp)
        );

        return query.Provider.CreateQuery<T>(resultExp);
    }

    public async Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedResultWithCountAsync(
        Expression<Func<T, bool>>? filter = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        try
        {
            IQueryable<T> query = _dbSet.AsNoTracking().Where(e => !e.IsDeleted);

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderByDescending(e => e.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in {Method} for entity type {Entity}", nameof(GetPaginatedResultWithCountAsync), typeof(T).Name);
            throw;
        }
    }
}