using System.Linq.Expressions;

namespace RentSaaS.Domain.Base;

public interface IRepository<T> where T : IEntity
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(long id);
    Task<bool> Add(T entity);
    Task<bool> Delete(long id);
    Task<bool> Upsert(T entity);
    Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
}
