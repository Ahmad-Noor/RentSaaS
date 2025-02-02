using RentSaaS.Domain.Base;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace RentSaaS.Infrastructure.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class, IEntity
{
    protected RentSaaSDBContext context;
    internal DbSet<T> dbSet;
    public readonly ILogger _logger;

    public Repository(RentSaaSDBContext context, ILogger logger)
    {
        this.context = context;
        dbSet = context.Set<T>();
        _logger = logger;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        //try
        //{
        return await dbSet.AsNoTracking().Where(c=>c.IsDeleted != true).ToListAsync();
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError(ex, "{Repo} All function error", typeof(UserRepository));
        //    return new List<T>();
        //}
    }


    public virtual async Task<T> GetById(Guid id)
    {

        //try
        //{
        T? entity = await dbSet.FindAsync(id);
        return entity;
        //}
        //catch (Exception e)
        //{
        //    _logger.LogError(e, "Error getting entity with id {Id}", id);
        //    return null;
        //}

    }

    public virtual async Task<bool> Add(T entity)
    {
        //try
        //{
        await dbSet.AddAsync(entity);
        return true;
        //}
        //catch (Exception e)
        //{
        //    _logger.LogError(e, "Error adding entity");
        //    return true;
        //} 
    }
    public Task<T> Update(T entity)
    {
        dbSet.Update(entity);
        return Task.FromResult(entity);
    }

    public async Task<bool> Delete(Guid id)
    {
        //try
        //{
        var entity = await dbSet.FindAsync(id);
        if (entity != null)
        {
            dbSet.Remove(entity);
            return true;
        }
        else
        {
            _logger.LogWarning("Entity with id {Id} not found for deletion", id);
            return false;
        }
        //}
        //catch (Exception e)
        //{
        //    _logger.LogError(e, "Error deleting entity with id {Id}", id);
        //    return false;
        //}
    }
    public async Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await dbSet.SingleOrDefaultAsync(predicate).ConfigureAwait(false);
    }
    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await dbSet.FirstOrDefaultAsync(expression);
    }
    public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }   
    public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate)
    {
        return await dbSet.Where(predicate).ToListAsync();
    }



}