using System.Linq.Expressions;

namespace RentSaaS.Domain.Base;

public interface IRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid id);
    Task<bool> Add(T entity);
    Task<bool> AddRangeAsync(T[] entities);
    Task<bool> Delete(Guid id);
    //Task<bool> Update(T entity);
    Task<T> Update(T entity);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);
}
