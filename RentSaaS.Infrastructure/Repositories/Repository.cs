using RentSaaS.Domain.Base;
using System.Linq.Expressions;  
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; 

namespace RentSaaS.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    { 
        protected RentSaaSDBContext context;
        internal DbSet<T> dbSet;
        public readonly ILogger _logger;

        public Repository( RentSaaSDBContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            _logger = logger;
        }

        public async Task<IEnumerable<T>> All()
        {
            //try
            //{
                return await dbSet.ToListAsync();
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
         
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public virtual Task<bool> Upsert(T entity)
        {
            throw new NotImplementedException();
        } 
    }
}