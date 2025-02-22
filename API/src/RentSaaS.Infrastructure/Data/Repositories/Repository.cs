//using RentSaaS.Domain.Base;
//using System.Linq.Expressions;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//namespace RentSaaS.Infrastructure.Data.Repositories;

//public class Repository<T> : IRepository<T> where T : class, IEntity
//{
//    protected RentSaaSDBContext context;
//    internal DbSet<T> dbSet;
//    public readonly ILogger _logger;

//    public Repository(RentSaaSDBContext context, ILogger logger)
//    {
//        this.context = context;
//        dbSet = context.Set<T>();
//        _logger = logger;
//    }

//    public IQueryable<T> AsQueryable()
//    {
//        return dbSet.AsNoTracking().Where(x => !x.IsDeleted);
//    }

//    public async Task<IEnumerable<T>> GetAll()
//    {
//        //try
//        //{
//        return await dbSet.AsNoTracking().Where(c => c.IsDeleted != true).ToListAsync();
//        //}
//        //catch (Exception ex)
//        //{
//        //    _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
//        //    return new List<T>();
//        //}
//    }


//    public virtual async Task<T> GetById(Guid id)
//    {

//        //try
//        //{
//        T? entity = await dbSet.FindAsync(id);
//        return entity;
//        //}
//        //catch (Exception e)
//        //{
//        //    _logger.LogError(e, "Error getting entity with id {Id}", id);
//        //    return null;
//        //}

//    }

//    public virtual async Task<bool> Add(T entity)
//    {
//        //try
//        //{
//        await dbSet.AddAsync(entity);
//        return true;
//        //}
//        //catch (Exception e)
//        //{
//        //    _logger.LogError(e, "Error adding entity");
//        //    return true;
//        //} 
//    }
//    public virtual async Task<bool> AddRangeAsync(T[] entities)
//    {
//        //try
//        //{
//        await dbSet.AddRangeAsync(entities);
//        return true;
//        //}
//        //catch (Exception e)
//        //{
//        //    _logger.LogError(e, "Error adding entity");
//        //    return true;
//        //} 
//    }
//    public Task<T> Update(T entity)
//    {
//        dbSet.Update(entity);
//        return Task.FromResult(entity);
//    }

//    public async Task<bool> Delete(Guid id)
//    {
//        //try
//        //{
//        var entity = await dbSet.FindAsync(id);
//        if (entity != null)
//        {
//            dbSet.Remove(entity);
//            return true;
//        }
//        else
//        {
//            _logger.LogWarning("Entity with id {Id} not found for deletion", id);
//            return false;
//        }
//        //}
//        //catch (Exception e)
//        //{
//        //    _logger.LogError(e, "Error deleting entity with id {Id}", id);
//        //    return false;
//        //}
//    }
//    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
//    {
//        return await dbSet.SingleOrDefaultAsync(predicate).ConfigureAwait(false);
//    }
//    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
//    {
//        return await dbSet.FirstOrDefaultAsync(expression);
//    }
//    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
//    {
//        return await dbSet.Where(predicate).ToListAsync();
//    }
//    public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
//    {
//        return await dbSet.Where(predicate).ToListAsync();
//    }

//    public async Task<IEnumerable<T>> GetAllPaginated(int page = 1, int pageSize = 10)
//    {
//        try
//        {
//            // Ensure page and pageSize are valid
//            page = page < 1 ? 1 : page;
//            pageSize = pageSize < 1 ? 10 : pageSize;

//            return await dbSet
//                .AsNoTracking()
//                .Where(c => !c.IsDeleted)
//                .OrderByDescending(e => e.CreatedAt) // Assuming you have CreatedAt field
//                .Skip((page - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "{Repo} GetAllPaginated function error", typeof(T).Name);
//            throw; // Rethrow to handle in controller
//        }
//    }

//    public async Task<int> Count(Expression<Func<T, bool>>? predicate = null)
//    {
//        try
//        {
//            if (predicate == null)
//            {
//                return await dbSet.AsNoTracking().Where(e => !e.IsDeleted).CountAsync();
//            }

//            return await dbSet.AsNoTracking().Where(e => !e.IsDeleted ).Where(predicate).CountAsync();
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "{Repo} Count function error", typeof(T).Name);
//            throw; // Rethrow to handle in controller
//        }
//    }

//    // Optional: Add method for filtered pagination
//    public async Task<IEnumerable<T>> GetAllPaginatedFiltered(
//        Expression<Func<T, bool>> filter,
//        int page = 1,
//        int pageSize = 10,
//        string? sortBy = null,
//        bool isAscending = true)
//    {
//        try
//        {
//            // Ensure page and pageSize are valid
//            page = page < 1 ? 1 : page;
//            pageSize = pageSize < 1 ? 10 : pageSize;

//            // Start with base query
//            var query = dbSet
//                .AsNoTracking()
//                .Where(e => !e.IsDeleted)
//                .Where(filter);

//            // Apply sorting if specified
//            if (!string.IsNullOrEmpty(sortBy))
//            {
//                // Assuming you have a method to handle dynamic sorting
//                query = ApplySorting(query, sortBy, isAscending);
//            }
//            else
//            {
//                // Default sorting by CreatedAt
//                query = isAscending
//                    ? query.OrderBy(e => e.CreatedAt)
//                    : query.OrderByDescending(e => e.CreatedAt);
//            }

//            return await query
//                .Skip((page - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "{Repo} GetAllPaginatedFiltered function error", typeof(T).Name);
//            throw; // Rethrow to handle in controller
//        }
//    }

//    // Helper method for dynamic sorting
//    private IQueryable<T> ApplySorting(IQueryable<T> query, string sortBy, bool isAscending)
//    {
//        // Get property info for the sortBy field
//        var property = typeof(T).GetProperty(sortBy,
//            System.Reflection.BindingFlags.IgnoreCase |
//            System.Reflection.BindingFlags.Public |
//            System.Reflection.BindingFlags.Instance);

//        if (property == null)
//        {
//            // If invalid property, fall back to CreatedAt
//            return isAscending
//                ? query.OrderBy(e => e.CreatedAt)
//                : query.OrderByDescending(e => e.CreatedAt);
//        }

//        // Create expression for sorting
//        var parameter = Expression.Parameter(typeof(T), "x");
//        var propertyAccess = Expression.Property(parameter, property);
//        var orderByExp = Expression.Lambda(propertyAccess, parameter);

//        // Apply ordering
//        var methodName = isAscending ? "OrderBy" : "OrderByDescending";
//        var resultExp = Expression.Call(
//            typeof(Queryable),
//            methodName,
//            new Type[] { typeof(T), property.PropertyType },
//            query.Expression,
//            Expression.Quote(orderByExp));

//        return query.Provider.CreateQuery<T>(resultExp);
//    }

//    // Optional: Add a method to get paginated result with total count
//    public async Task<(IEnumerable<T> Items, int TotalCount)> GetPaginatedResultWithCount(
//        Expression<Func<T, bool>>? filter = null,
//        int page = 1,
//        int pageSize = 10)
//    {
//        try
//        {
//            var query = dbSet.AsNoTracking().Where(e => !e.IsDeleted);

//            if (filter != null)
//            {
//                query = query.Where(filter);
//            }

//            var totalCount = await query.CountAsync();

//            var items = await query
//                .OrderByDescending(e => e.CreatedAt)
//                .Skip((page - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();

//            return (items, totalCount);
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "{Repo} GetPaginatedResultWithCount function error", typeof(T).Name);
//            throw; // Rethrow to handle in controller
//        }
//    }

//}
using RentSaaS.Domain.Base;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RentSaaS.Infrastructure.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly RentSaaSDBContext _context;
        protected readonly DbSet<T> _dbSet;
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
}