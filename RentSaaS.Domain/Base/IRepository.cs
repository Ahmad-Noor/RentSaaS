using System.Linq.Expressions;

namespace RentSaaS.Domain.Base
{
    public interface IRepository<T> where T : IEntity
    { 
        Task<IEnumerable<T>> All();
        Task<T> GetById(Guid id);
        Task<bool> Add(T entity);
        Task<bool> Delete(Guid id);
        Task<bool> Upsert(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
         
    }
}
